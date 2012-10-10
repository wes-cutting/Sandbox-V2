using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rentler.Facades;
using Rentler.Data;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using Rentler.Helpers;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;

namespace KslListingImporter
{
	public class ImageHelper
	{
		//get blob container
		static CloudStorageAccount account = CloudStorageAccount.FromConfigurationSetting("BlobConnectionString");
		static CloudBlobClient client = account.CreateCloudBlobClient();
		static CloudBlobContainer container = CreateContainer();

		static CloudBlobContainer CreateContainer()
		{
			var c = client.GetContainerReference("userphotos");

			BlobContainerPermissions permissions = new BlobContainerPermissions()
			{
				PublicAccess = BlobContainerPublicAccessType.Container
			};

			c.SetPermissions(permissions);

			return c;
		}

		public static Photo ProcessPhoto(int buildingId, byte[] imageBytes, ImageFormat format, string extension,
			BuildingFacade buildingFacade, PhotoFacade photoFacade)
		{
			var photoId = Guid.NewGuid();

			//resolutions
			var vectors = new List<Vector2>();
			vectors.Add(new Vector2(800, 600));
			vectors.Add(new Vector2(600, 395));
			vectors.Add(new Vector2(280, 190));
			vectors.Add(new Vector2(200, 150));
			vectors.Add(new Vector2(115, 85));
			vectors.Add(new Vector2(50, 50));

			//load image
			using(var stream = new MemoryStream(imageBytes))
			{
				stream.Position = 0;
				using(Image image = Image.FromStream(stream))
				{
					foreach(var v in vectors)
					{
						using(Image resized = ImageScaler.Crop(image, v.X, v.Y,
								  ImageScaler.AnchorPosition.Center))
						{
							string fileName = string.Format("{0}-{1}x{2}{3}",
												photoId, v.X, v.Y, extension);

							//UploadPhoto would go here
							SavePhoto(buildingId, resized, fileName, format);
						}
					}
				}
			}

			//create the db reference for the images.
			var photo = new Photo();
			photo.PhotoId = photoId;
			photo.BuildingId = buildingId;
			photo.Extension = extension.Replace(".",string.Empty);

			if(photoFacade.GetPhotos(buildingId).Count(p => p.IsPrimary) == 0)
				photo.IsPrimary = true;

			if(photo.IsPrimary)
			{
				var building = buildingFacade.GetBuilding(buildingId);
				building.PrimaryPhotoId = photo.PhotoId;
				building.PrimaryPhotoExtension = photo.Extension;
				buildingFacade.Save();
			}

			photoFacade.AddPhoto(buildingId, photo);
			photoFacade.Save();
			return photo;
		}

		static void UploadPhoto(Image pic, string filename, ImageFormat format)
		{
			//convert image to bytes
			using(MemoryStream ms = new MemoryStream())
			{
				pic.Save(ms, format);
				ms.Position = 0;

				//create the blob, set metadata and properties
				var blob = container.GetBlobReference(filename);
				blob.Metadata["Filename"] = filename;
				blob.Properties.ContentType = MimeHandler.GetContentType(Path.GetExtension(filename));

				//upload!
				blob.UploadFromStream(ms);
				blob.SetMetadata();
				blob.SetProperties();
			}
		}

		static void SavePhoto(int buildingId, Image pic, string filename, ImageFormat format)
		{
			//ensure the main directory is there
			System.IO.Directory.CreateDirectory("userphotostest");

			//separate files 10,000 at a time
			System.IO.Directory.CreateDirectory(string.Format("userphotostest\\{0}", (ImageCounter.ImageCount / 10000)));

			//create directory for the building, to match the hierarchy in azure
			System.IO.Directory.CreateDirectory(string.Format("userphotostest\\{0}\\{1}",
				(ImageCounter.ImageCount / 10000), buildingId));

			pic.Save(string.Format("userphotostest\\{0}\\{1}\\",
				(ImageCounter.ImageCount / 10000), buildingId) + filename, format);
			ImageCounter.ImageCount++;
		}
	}
}
