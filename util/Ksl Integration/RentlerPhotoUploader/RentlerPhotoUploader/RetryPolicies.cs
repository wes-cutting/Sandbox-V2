using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.WindowsAzure.StorageClient;

namespace RentlerPhotoUploader
{
	public static class RetryPolicies
	{
		static ShouldRetry RetryAlwaysImplementation()
		{
			var should = new ShouldRetry((int number, Exception exc, out TimeSpan delay) =>
			{
				delay = TimeSpan.FromMinutes(2);
				Console.WriteLine("Error: " + exc.Message);
				Console.WriteLine("Retrying in {0} minutes", delay.Minutes);
				return true;
			});

			return should;
		}

		public static RetryPolicy RetryAlways()
		{
			var retry = new RetryPolicy(RetryAlwaysImplementation);

			return retry;
		}
	}
}
