using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Rentler
{
    public enum ContactInfoType
    {
        [Description("Any")]
        Undefined = 0,

        [Description("Personal")]
        Personal = 1,
        
        [Description("Professional")]
        Professional = 2
    }
}
