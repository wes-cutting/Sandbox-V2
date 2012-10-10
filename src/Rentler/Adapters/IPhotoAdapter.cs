using System;
using System.Collections.Generic;
using System.Linq;
using Rentler.Data;
using System.Web;
using System.IO;
using Rentler.Azure;
using System.Drawing;
using Rentler.Drawing;
using System.Drawing.Imaging;
using Rentler.Web;
using Rentler.Redis;
using Rentler.Configuration;
using Rentler.Auth;


namespace Rentler.Adapters
{
    public interface IPhotoAdapter
    {
        Status<Photo[]> UploadPhotos(string username, long buildingId, UploadFileStream[] files);

        Status<bool> RemovePhoto(string username, Guid photoId);

        Status<Photo> SetSortOrder(Guid photoId, int sortOrder);

        Status<string[]> ReorderPhotos(string username, Guid[] photoIds);

        Status<string[]> GetOrderedPhotoIds(long buildingId);
    }

    public class PhotoAdapter : IPhotoAdapter
    {
        public Status<Photo[]> UploadPhotos(string username, long buildingId, UploadFileStream[] files)
        {
            using (var context = new RentlerContext())
            {
                try
                {
                    // get the building
                    var building = (from b in context.Buildings
                                    where b.User.Username == username &&
                                    b.BuildingId == buildingId
                                    select b).SingleOrDefault();

                    if (building == null)
                        return Status.NotFound<Photo[]>();

                    int nextPosition = building.Photos.Count;

                    List<Photo> final = new List<Photo>();

                    foreach (var upload in files)
                    {
                        if (upload == null) continue;

                        Photo photo = new Photo();
                        photo.Extension = Path.GetExtension(upload.FileName).ToLower();
                        photo.BuildingId = building.BuildingId;
                        photo.CreateDateUtc = DateTime.UtcNow;
                        photo.CreatedBy = "photoadapter";
                        photo.UpdateDateUtc = DateTime.UtcNow;
                        photo.UpdatedBy = "photoadapter";
                        photo.PhotoId = Guid.NewGuid();
                        photo.SortOrder = nextPosition;

                        if (!Rentler.Configuration.Photos.Current.SupportedExtensions.Contains(photo.Extension))
                            continue;

                        UploadPhotoToAzure(upload.Stream, photo, 800, 600);
                        UploadPhotoToAzure(upload.Stream, photo, 600, 395);
                        UploadPhotoToAzure(upload.Stream, photo, 280, 190);
                        UploadPhotoToAzure(upload.Stream, photo, 200, 150);
                        UploadPhotoToAzure(upload.Stream, photo, 115, 85);
                        UploadPhotoToAzure(upload.Stream, photo, 50, 50);

                        building.Photos.Add(photo);

                        // see if it's the first photo uploaded
                        if (nextPosition == 0)
                        {
                            building.PrimaryPhotoId = photo.PhotoId;
                            building.PrimaryPhotoExtension = photo.Extension;
                        }

                        context.SaveChanges();

                        photo.Building = null;
                        final.Add(photo);

                        nextPosition++;
                    }

                    InvalidateCache(buildingId);

                    return Status.OK<Photo[]>(final.ToArray());
                }
                catch (Exception ex)
                {
                    return Status.Error<Photo[]>(ex.Message, null);
                }
            }
        }

        private void UploadPhotoToAzure(Stream file, Photo photo, int width, int height)
        {
            // create a resized image
            using (Image resized = ImageScaler.Crop(
                Image.FromStream(file),
                width, height, ImageScaler.AnchorPosition.Center))
            {
                using (MemoryStream upload = new MemoryStream())
                {
                    // encode and save that image to a memory stream
                    ImageCodecInfo[] info = ImageCodecInfo.GetImageEncoders();
                    var encoderParams = new EncoderParameters(1);
                    encoderParams.Param[0] = new EncoderParameter(Encoder.Quality, 100L);
                    resized.Save(upload, info[1], encoderParams);

                    // reset the image
                    upload.Position = 0;

                    Rentler.Azure.BlobAdapter.UploadPhoto(photo, upload, width, height);
                }
            }
        }

        private bool HasFile(HttpPostedFileBase file)
        {
            return (file != null && file.ContentLength > 0) ? true : false;
        }


        public Status<bool> RemovePhoto(string username, Guid photoId)
        {
            if (string.IsNullOrWhiteSpace(username))
                return Status.ValidationError<bool>(false, "username", "User not authenticated");

            using (RentlerContext context = new RentlerContext())
            {
                try
                {
                    var building = (from p in context.Photos
                                        .Include("Building.Photos")
                                    where p.PhotoId == photoId &&
                                    p.Building.User.Username == username
                                    select p.Building).SingleOrDefault();

                    if (building == null)
                        return Status.NotFound<bool>();

                    var photo = building.Photos.SingleOrDefault(p => p.PhotoId == photoId);

                    if (photo == null)
                        return Status.NotFound<bool>();

                    context.Photos.Remove(photo);

                    // primary photo is being removed
                    if (building.PrimaryPhotoId == photoId)
                    {
                        // get new primary photo (next in line)
                        var nextPhoto = (from p in building.Photos
                                         where p.PhotoId != photoId
                                         orderby p.SortOrder
                                         select p).FirstOrDefault();

                        // if there isn't one then it doesn't matter
                        if (nextPhoto == null)
                        {
                            building.PrimaryPhotoId = null;
                            building.PrimaryPhotoExtension = null;
                        }
                        else
                        {
                            building.PrimaryPhotoId = nextPhoto.PhotoId;
                            building.PrimaryPhotoExtension = nextPhoto.Extension;
                        }
                    }

                    context.SaveChanges();

                    Rentler.Azure.BlobAdapter.RemovePhoto(photo, 800, 600);
                    Rentler.Azure.BlobAdapter.RemovePhoto(photo, 600, 395);
                    Rentler.Azure.BlobAdapter.RemovePhoto(photo, 280, 190);
                    Rentler.Azure.BlobAdapter.RemovePhoto(photo, 200, 150);
                    Rentler.Azure.BlobAdapter.RemovePhoto(photo, 115, 85);
                    Rentler.Azure.BlobAdapter.RemovePhoto(photo, 50, 50);

                    InvalidateCache(building.BuildingId);

                    return Status.OK<bool>(true);
                }
                catch (Exception ex)
                {
                    return Status.Error<bool>(ex.Message, false);
                }
            }
        }

        public Status<Photo> SetSortOrder(Guid photoId, int sortOrder)
        {
            using (RentlerContext context = new RentlerContext(false))
            {
                try
                {
                    var photo = (from p in context.Photos
                                 where p.PhotoId == photoId
                                 select p).SingleOrDefault();

                    photo.SortOrder = sortOrder;

                    context.SaveChanges();

                    InvalidateCache(photo.BuildingId);

                    return Status.OK<Photo>(photo);
                }
                catch (Exception ex)
                {
                    return Status.Error<Photo>(ex.Message, null);
                }
            }
        }


        public Status<string[]> ReorderPhotos(string username, Guid[] photoIds)
        {
            if (photoIds.Length == 0)
                return Status.ValidationError<string[]>(null, "photoIds", "photoIds cannot be empty");

            string[] existingPhotoIds = null;
            Guid primaryPhotoId = photoIds[0];

            using (RentlerContext context = new RentlerContext())
            {
                try
                {
                    var building = (from p in context.Photos
                                        .Include("Building.Photos")
                                    where p.PhotoId == primaryPhotoId &&
                                    p.Building.User.Username == username
                                    select p.Building).SingleOrDefault();

                    if (building == null)
                        return Status.NotFound<string[]>();

                    // save order prior to changing in case we have to restore them
                    var photos = from p in building.Photos
                                 orderby p.SortOrder
                                 select p;

                    existingPhotoIds = photos.Select(p => p.PhotoId.ToString()).ToArray();

                    for (int i = 0; i < photoIds.Length; ++i)
                    {
                        var photo = photos.SingleOrDefault(p => p.PhotoId == photoIds[i]);
                        photo.SortOrder = i;

                        // primary photo
                        if (i == 0)
                        {
                            building.PrimaryPhotoId = photo.PhotoId;
                            building.PrimaryPhotoExtension = photo.Extension;
                        }
                    }

                    context.SaveChanges();

                    string[] resultIds = photoIds.Select(i => i.ToString()).ToArray();

                    InvalidateCache(building.BuildingId);

                    return Status.OK<string[]>(resultIds);
                }
                catch (Exception ex)
                {
                    return Status.Error<string[]>(ex.Message, existingPhotoIds);
                }
            }
        }

        public Status<string[]> GetOrderedPhotoIds(long buildingId)
        {
            var identity = CustomAuthentication.GetIdentity();

            if (!identity.IsAuthenticated)
                return Status.UnAuthorized<string[]>();

            if (buildingId == 0)
                return Status.ValidationError<string[]>(null, "buildingId", "buildingId cannot be empty");

            using (RentlerContext context = new RentlerContext())
            {
                try
                {                    
                    var building = (from b in context.Buildings.Include("Photos")
                                    where b.BuildingId == buildingId
                                    select b).SingleOrDefault();

                    if(building == null)
                        return Status.NotFound<string[]>();

                    var photoIds = building.Photos
                        .OrderBy(p => p.SortOrder)
                        .Select(p => p.PhotoId.ToString())
                        .ToArray();

                    return Status.OK<string[]>(photoIds);
                }
                catch (Exception ex)
                {
                    return Status.Error<string[]>(ex.Message, null);
                }
            }
        }

        void InvalidateCache(long buildingId)
        {
            //invalidate L2 cache
            var connection = ConnectionGateway.Current.GetWriteConnection();
            var task = connection.Keys.Remove(App.RedisCacheDatabase, CacheKeys.LISTING + buildingId);
            connection.Wait(task);
        }
    }
}
