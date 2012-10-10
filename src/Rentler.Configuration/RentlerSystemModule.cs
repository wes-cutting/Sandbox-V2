using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject.Modules;
using Ninject.Activation;
using Rentler.Adapters;
using Rentler.Web.Email;
using Rentler.Facades;
using Rentler.Data;

namespace Rentler.Configuration
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

            // facade bindings - Will eventually be converted
            // to adapters.
            Bind<IPropertyFacade>().To<PropertyFacade>();
            Bind<IAccountFacade>().To<AccountFacade>();
            Bind<IListingFacade>().To<ListingFacade>();
            Bind<IRoleFacade>().To<RoleFacade>();
            Bind<IUserFacade>().To<UserFacade>();

            Bind<IDataServiceFactory>().To<SqlDataServiceFactory>();

            //email bindings
            Bind<IAccountMailer>().To<Rentler.Web.Email.ServerAccountMailer>();
            Bind<IOrderMailer>().To<Rentler.Web.Email.ServerOrderMailer>();
            Bind<IListingMailer>().To<Rentler.Web.Email.ServerListingMailer>();
            Bind<IPropertyMailer>().To<Rentler.Web.Email.ServerPropertyMailer>();
        }

        public string GetParentTypeName(IContext context)
        {
            return context.Request.ParentContext.Request.ParentContext.Request.Service.FullName;
        }
    }
}
