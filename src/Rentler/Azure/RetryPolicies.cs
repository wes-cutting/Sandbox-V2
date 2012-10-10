using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.WindowsAzure.StorageClient;

namespace Rentler.Azure
{
    public static class RetryPolicies
    {
        static ShouldRetry RetryAlwaysImplementation()
        {
            var should = new ShouldRetry((int number, Exception exc, out TimeSpan delay) =>
            {
                delay = TimeSpan.FromMinutes(2);
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
