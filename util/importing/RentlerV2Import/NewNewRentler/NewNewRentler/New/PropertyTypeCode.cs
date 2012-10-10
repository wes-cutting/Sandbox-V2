using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewNewRentler.New
{
	public enum PropertyType
	{
		Undefined = 0,
		SingleFamilyHome = 1,
		Apartment = 2,
		CondoTownhome = 3,
		MultiFamilyHome = 4,
		ManufacturedHome = 5,
		HorseLivestock = 6,
		SingleRoom = 7
	}

	public static class PropertyInfoConverter
	{
		public static PropertyType Convert(string type)
		{
			PropertyType p = PropertyType.Undefined;

			switch(type)
			{
				case "Apartment":
					p = PropertyType.Apartment;
					break;
				case "Condo/Townhome":
					p = PropertyType.CondoTownhome;
					break;
				case "Manufactured Home":
					p = PropertyType.ManufacturedHome;
					break;
				case "Single Room" :
					p = PropertyType.SingleRoom;
					break;
				case "Single-Family Home" :
					p = PropertyType.SingleFamilyHome;
					break;
				case "Horse/Livestock" :
					p = PropertyType.HorseLivestock;
					break;
				case "Multi-Family Home" :
					p = PropertyType.MultiFamilyHome;
					break;
				default:
					break;
			}

			return p;
		}
	}
}
