using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject.Modules;
using Ninject.Activation;
using Rentler.Adapters;
using Rentler.Adapters.Core;
using Rentler.Web.Email;

namespace Rentler.Web.App_Start
{
    /// <summary>
    /// Dependency injection bindings for the application.
    /// </summary>
    public class RentlerSystemModule : NinjectModule
    {
        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public override void Load()
        {
            // adapter bindings
            Bind<IAccountAdapter>().To<AccountAdapter>();
            Bind<IListingAdapter>().To<ListingAdapter>();
            Bind<IFriendlyZipAdapter>().To<FriendlyZipAdapter>();
            Bind<ISearchAdapter>().To<SearchAdapter>();
            Bind<IPropertyAdapter>().To<PropertyAdapter>();
			Bind<IAuthAdapter>().To<AuthAdapter>();
            Bind<IUnitAdapter>().To<UnitAdapter>();
			Bind<IListingStatsAdapter>().To<ListingStatsAdapter>();
			Bind<IFeaturedAdapter>().To<FeaturedAdapter>();
            Bind<IOrderAdapter>().To<OrderAdapter>();
            Bind<IPhotoAdapter>().To<PhotoAdapter>();

			//email bindings
			Bind<IAccountMailer>().To<Rentler.Web.Email.ServerAccountMailer>();
			Bind<IOrderMailer>().To<Rentler.Web.Email.ServerOrderMailer>();
			Bind<IListingMailer>().To<Rentler.Web.Email.ServerListingMailer>();
        }

        public string GetParentTypeName(IContext context)
        {
            return context.Request.ParentContext.Request.ParentContext.Request.Service.FullName;
        }
    }
}