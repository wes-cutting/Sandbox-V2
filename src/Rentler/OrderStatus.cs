using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rentler
{
    public enum OrderStatus
    {
        New = 0,
        Pending = 1,
        Succeeded = 2,
        Failed = 3,
        CardDeclined = 4,
        ServiceUnavailable = 5
    }
}
