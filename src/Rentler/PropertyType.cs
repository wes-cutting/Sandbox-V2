using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Rentler
{
    public enum PropertyType
    {
        [Description("Property Type")]
        Undefined = 0,

        [Description("Single-Family Home")]
        SingleFamilyHome = 1,

        [Description("Apartment")]
        Apartment = 2,

        [Description("Condo/Townhome")]
        CondoTownhome = 3,

        [Description("Multi-Family Home")]
        MultiFamilyHome = 4,

        [Description("Manufactured Home")]
        ManufacturedHome = 5,

        [Description("Horse/Livestock")]
        HorseLivestock = 6,

        [Description("Single Room")]
        SingleRoom = 7
    }
}
