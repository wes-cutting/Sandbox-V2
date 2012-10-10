using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewNewRentler.New
{
	public class ProductsConverter
	{
		public static string Convert(int i)
		{
			string product = string.Empty;

			switch (i)
			{
				case 2:
					product = "ribbon";
					break;
				case 3:
					product = "featureddate";
					break;
			}

			return product;
		}
	}
}
