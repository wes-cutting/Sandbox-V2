using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.WindowsAzure.StorageClient;
using Rentler.Configuration;
using Microsoft.WindowsAzure;
using System.IO;
using Rentler.Web;

namespace Rentler.Azure
{
    /// <summary>
    /// Class for getting blob information.
    /// </summary>
    public static class BlobAdapter
    {        
        /// <summary>
        /// Gets the specified blob from the image information.
        /// </summary>
        /// <param name="buildingId">The building id.</param>
        /// <param name="imageId">The image id.</param>
        /// <param name="extension">The extension.</param>
        /// <returns>A cloudblob object directed at the extension.</returns>
        public static CloudBlob GetFromImageInfo(
            long buildingId, Guid imageId, string extension, int width, int height)
        {            
            // grab the container
            CloudBlobContainer container = CloudStorageAccount
                .Parse(App.BlobConnection)
                .CreateCloudBlobClient()
                .GetContainerReference(App.PhotoFolder.ToLower());

            if (container.CreateIfNotExist())
            {
                container.SetPermissions(new BlobContainerPermissions()
                {
                    PublicAccess = BlobContainerPublicAccessType.Blob
                });
            }
            
            return container.GetBlobReference(
                string.Format("{0}/{1}-{2}x{3}{4}", buildingId, imageId, width, height, extension));
        }

        public static CloudBlob UploadPhoto(Photo photo, MemoryStream stream, int width, int height)
        {
            // set up the azure storage
            var blob = Rentler.Azure.BlobAdapter.GetFromImageInfo(
                photo.BuildingId, photo.PhotoId, photo.Extension, width, height);

            string filename = string.Format("{0}-{1}x{2}{3}", photo.PhotoId, width, height, photo.Extension);

            //create blob
            blob.Metadata["Filename"] = Path.GetFileName(filename);
            blob.Properties.ContentType = MimeHandler.GetContentType(Path.GetExtension(filename));

            // set the options
            var options = new BlobRequestOptions();
            options.RetryPolicy = RetryPolicies.RetryAlways();

            // upload
            blob.UploadFromStream(stream, options);
            blob.SetMetadata();
            blob.SetProperties();

            return blob;
        }

        public static void RemovePhoto(Photo photo, int width, int height)
        {
            var blob = GetFromImageInfo(photo.BuildingId, photo.PhotoId, photo.Extension, width, height);
            blob.DeleteIfExists();
        }
    }
}
