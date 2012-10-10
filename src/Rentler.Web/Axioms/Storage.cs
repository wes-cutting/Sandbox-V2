using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Rentler.Configuration;

namespace Rentler.Web.Axioms
{
    public static class Storage
    {
        public static string GetPhotoLink(long buildingId, Guid photoId, 
            int width, int height, string extension)
        {
            extension = extension.Replace(".", "");
          
            return string.Format(
                "{0}{1}/{2}/{3}-{4}x{5}.{6}",
                App.BlobStorageHostname, App.PhotoFolder,
                buildingId, photoId,
                width, height, extension);
        }
    }
}