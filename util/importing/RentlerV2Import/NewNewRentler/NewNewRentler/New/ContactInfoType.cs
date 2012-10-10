using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewNewRentler.New
{

	public enum ContactInfoType
	{
		Any = 0,
		Personal = 1,
		Professional = 2
	}

	public static class ContactInfoTypeConverter
	{
		public static ContactInfoType Convert(string type)
		{
			var p = ContactInfoType.Any;

			switch(type)
			{
				case "Personal":
					p = ContactInfoType.Personal;
					break;
				case "Professional":
					p = ContactInfoType.Professional;
					break;
				default:
					p = ContactInfoType.Any;
					break;
			}

			return p;
		}
	}
}
