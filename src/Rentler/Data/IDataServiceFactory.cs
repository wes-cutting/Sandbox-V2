using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rentler.Data
{
    /// <summary>
    /// Factory for instantiating a data service. The
    /// concrete implementation of this is handled by ninject.
    /// This allows you to get a service factory injected into
    /// an adapter and then you can use a using statement around
    /// the Get method to retain disposing.
    /// </summary>
    public interface IDataServiceFactory
    {
        /// <summary>
        /// Instantiates a new instance of the data service.
        /// </summary>
        /// <returns>A new instance of the service.</returns>
        IDataService Get();
    }
}
