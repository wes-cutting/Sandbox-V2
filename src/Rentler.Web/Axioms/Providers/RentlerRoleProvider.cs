using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Rentler.Facades;
using System.Configuration.Provider;

namespace Rentler.Web.Axioms.Providers
{
	public class RentlerRoleProvider : RoleProvider
	{
		IRoleFacade roleFacade;

		public RentlerRoleProvider()
		{
			roleFacade = new RoleFacade();
		}

		public override void AddUsersToRoles(string[] usernames, string[] roleNames)
		{
			roleFacade.AddUsersToRoles(usernames, roleNames);
			roleFacade.Save();
		}

		public override string ApplicationName
		{
			get
			{
				return "Rentler";
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public override void CreateRole(string roleName)
		{
			roleFacade.AddRole(roleName);
			roleFacade.Save();
		}

		public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
		{
			if (throwOnPopulatedRole && roleFacade.AnyUsersInRole(roleName))
				throw new ProviderException("Cannot delete role. There are currently users in this role.");

			roleFacade.DeleteRole(roleName);
			roleFacade.Save();

			return true;
		}

		public override string[] FindUsersInRole(string roleName, string usernameToMatch)
		{
			var users = roleFacade.FindUsersInRole(roleName, usernameToMatch);

			return users;
		}

		public override string[] GetAllRoles()
		{
			var roles = roleFacade.GetRoles();

			return roles;
		}

		public override string[] GetRolesForUser(string username)
		{
			var roles = roleFacade.GetRolesForUser(username);

			return roles;
		}

		public override string[] GetUsersInRole(string roleName)
		{
			var users = roleFacade.GetUsersInRole(roleName);

			return users;
		}

		public override bool IsUserInRole(string username, string roleName)
		{
			var isInRole = roleFacade.IsUserInRole(username, roleName);

			return isInRole;
		}

		public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
		{
			roleFacade.RemoveUsersFromRoles(usernames, roleNames);
			roleFacade.Save();
		}

		public override bool RoleExists(string roleName)
		{
			var exists = roleFacade.RoleExists(roleName);

			return exists;
		}
	}
}