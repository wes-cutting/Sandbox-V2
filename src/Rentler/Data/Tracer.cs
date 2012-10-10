using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rentler.Data
{
    public static class Tracer
    {
        public static void OutputQuery<T>(IQueryable<T> query)
        {
            System.Data.Entity.Infrastructure.DbQuery<T> dbQuery =
                query as System.Data.Entity.Infrastructure.DbQuery<T>;

            if (dbQuery != null)
            {
                System.Diagnostics.Debug.WriteLine(dbQuery.ToString());
            }
        }
    }
}
