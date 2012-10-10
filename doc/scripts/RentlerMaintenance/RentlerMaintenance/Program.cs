using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RentlerMaintenance
{
	class Program
	{
		static void Main(string[] args)
		{
			using (var context = new RentlerDataContext())
			{

				// get local date, this will be stored in product description (not utc date)
				TimeZoneInfo mstTZ = TimeZoneInfo.FindSystemTimeZoneById("Mountain Standard Time");
				DateTime dateLocal = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, mstTZ);

				//deactivate old listings
				var activeCount = context.Buildings
					.Where(b => b.IsActive && 
								!b.IsDeleted && 
								b.DateActivatedUtc < dateLocal.AddMonths(-1)).Count();
				context.ExecuteCommand(
					@"	update Buildings 
						set IsActive = 0 
						where	DateActivatedUtc < {0} and
								IsActive = 1 and 
								IsDeleted = 0", dateLocal.AddMonths(-1));

				Console.WriteLine("Deactivated {0} listings", activeCount);

				//turn off expired priority listings
				var priorityCount = context.Buildings
					.Where(b => b.IsActive &&
								!b.IsDeleted &&
								b.DatePrioritized < dateLocal.AddMonths(-1)).Count();

				context.ExecuteCommand(@"
						update	Buildings
						set		HasPriority = 0
						where	DatePrioritized < {0}
				", dateLocal.AddMonths(-1));

				Console.WriteLine("Turned off {0} prioritized properties", priorityCount);

				//turn off expired ribbons
				var ribbonCount = context.Buildings
					.Where(b => b.DateRibbonActivated < dateLocal.AddMonths(-1) && 
								b.RibbonId != null).Count();

				context.ExecuteCommand(@"
						update	Buildings
						set		Ribbonid = null
						where	DateRibbonActivated < {0}", dateLocal.AddMonths(-1));

				Console.WriteLine("Removed {0} expired ribbons", ribbonCount);

				Console.WriteLine("Done!");
				Console.ReadLine();
			}
		}
	}
}
