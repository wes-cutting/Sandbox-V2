using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rentler.Data
{
    public class SqlDataServiceFactory : IDataServiceFactory
    {
        public IDataService Get()
        {
            return new SqlDataService();
        }
    }
}
