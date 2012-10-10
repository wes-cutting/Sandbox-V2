using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rentler.Web
{
    /// <summary>
    /// Handles the MIME types of the internet.
    /// </summary>
    public static class MimeHandler
    {
        /// <summary>
        /// Gets the content type for any known extension.
        /// </summary>
        /// <param name="extension">The type.</param>
        /// <returns>The mime type.</returns>
        public static string GetContentType(string extension)
        {
            return GetTypes()[extension.ToLower()];
        }

        /// <summary>
        /// Gets the extension for any known content type.
        /// </summary>
        /// <param name="contentType">Type of the content.</param>
        /// <returns></returns>
        public static string GetExtension(string contentType)
        {
            return GetTypes().Single(n => n.Value == contentType).Key;
        }

        static Dictionary<string, string> GetTypes()
        {
            return Rentler.Configuration.MimeTypes.Instance.Types;
        }
    }
}
