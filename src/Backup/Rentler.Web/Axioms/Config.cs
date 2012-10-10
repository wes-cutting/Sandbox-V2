using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace Rentler.Web
{
    public static class Config
    {        
        private static string addThisPubId;
        private static string hostname;

        public static string Hostname
        {
            get
            {
                if (string.IsNullOrWhiteSpace(hostname))
                    hostname = ConfigurationManager.AppSettings["Hostname"];

                return hostname;
            }
        }
        
        public static string AddThisPubId
        {
            get
            {
                if (string.IsNullOrEmpty(addThisPubId))
                    addThisPubId = ConfigurationManager.AppSettings["AddThisPubId"];
                
                return addThisPubId;
            }
        }
    }
}