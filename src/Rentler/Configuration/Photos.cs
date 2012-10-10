using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rentler.Configuration
{
    public sealed class Photos
    {
        private static volatile Photos instance;
        private static object syncRoot = new Object();

        public Photos()
        {
            Configure();            
        }

        private void Configure()
        {
            List<string> supported = new List<string>();
            supported.Add(".jpg");
            supported.Add(".jpeg");
            supported.Add(".gif");
            supported.Add(".png");
            this.SupportedExtensions = supported.ToArray();            
        }

        public string[] SupportedExtensions { get; set; }

        /// <summary>
        /// Gets the current instance. Uses
        /// the double locking approach as it is safe and handled in .Net
        /// without volitility.
        /// </summary>
        public static Photos Current
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new Photos();
                    }
                }
                return instance;
            }
        }
    }
}
