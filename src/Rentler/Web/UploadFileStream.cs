using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Rentler.Web
{
    public class UploadFileStream
    {
        public Stream Stream { get; set; }

        public string FileName { get; set; }
    }
}
