using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rentler.Configuration
{
    /// <summary>
    /// Configuration for all the ribbons used in the system.
    /// Implemented as a Singleton for universal access and multithreaded
    /// instantiation.
    /// </summary>
    public sealed class Ribbons
    {
        private static volatile Ribbons instance;
        private static object syncRoot = new Object();

        /// <summary>
        /// Prevents a default instance of the <see cref="Ribbons"/> class from being created.
        /// </summary>
        private Ribbons()
        {
            this.AvailableRibbons = new Dictionary<string, string>();
            this.ConfigureRibbons();
        }

        /// <summary>
        /// Configures the ribbons.
        /// </summary>
        private void ConfigureRibbons()
        {            
            AvailableRibbons.Add("3cargarage", "3 Car Garage");
            AvailableRibbons.Add("bigyard", "Big Yard");
            AvailableRibbons.Add("brandnew", "Brand New");
            AvailableRibbons.Add("greatschools", "Great Schools");
            AvailableRibbons.Add("nearapark", "Near a Park");
            AvailableRibbons.Add("noyardwork", "No Yardwork");
            AvailableRibbons.Add("petfriendly", "Pet Friendly");
            AvailableRibbons.Add("snowremoval", "Snow Removal");
            AvailableRibbons.Add("utilitiesincluded", "Utilities Included");
            AvailableRibbons.Add("newlyremodeled", "Newly Remodeled");
            AvailableRibbons.Add("airconditioning",	"Air Conditioning");
            AvailableRibbons.Add("clubhouse", "Clubhouse");
            AvailableRibbons.Add("fencedyard", "Fenced Yard");
            AvailableRibbons.Add("firstmonthfree", "First Month Free");
            AvailableRibbons.Add("fitnesscenter", "Fitness Center");
            AvailableRibbons.Add("granitecountertops", "Granite Countertops");
            AvailableRibbons.Add("hardwoodfloors", "Hardwood Floors");
            AvailableRibbons.Add("hottub", "Hot Tub");
            AvailableRibbons.Add("monthtomonth", "Month to Month");
            AvailableRibbons.Add("newcarpet", "New Carpet");            
            AvailableRibbons.Add("newpaint", "New Paint");
            AvailableRibbons.Add("onsitemaintenance", "Onsite Maintenance");
            AvailableRibbons.Add("onsitemanager", "Onsite Manager");
            AvailableRibbons.Add("playground", "Playground");
            AvailableRibbons.Add("pool", "Pool");
            AvailableRibbons.Add("smokerfriendly", "Smoker Friendly");
            AvailableRibbons.Add("stainlessappliances", "Stainless Appliances");
            AvailableRibbons.Add("washerdryer", "Washer/Dryer");
        }

        /// <summary>
        /// Gets or sets the available ribbons.
        /// </summary>
        /// <value>
        /// The available ribbons.
        /// </value>
        public Dictionary<string, string> AvailableRibbons { get; set; }

        /// <summary>
        /// Gets the current instance of the ribbons configuration. Uses
        /// the double locking approach as it is safe and handled in .Net
        /// without volitility.
        /// </summary>
        public static Ribbons Current
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new Ribbons();
                    }
                }
                return instance;
            }
        }
    }
}
