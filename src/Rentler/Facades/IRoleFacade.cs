using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rentler.Data;

namespace Rentler.Facades
{
	public interface IRoleFacade
	{
		void AddRole(string roleName);
		void AddUsersToRoles(string[] usernames, string[] roleNames);
		bool AnyUsersInRole(string roleName);
		void DeleteRole(string roleName);
		string[] FindUsersInRole(string roleName, string username);
		string[] GetRoles();
		string[] GetRolesForUser(string username);
		string[] GetUsersInRole(string roleName);
		bool IsUserInRole(string username, string roleName);
		void RemoveUsersFromRoles(string[] usernames, string[] roleNames);
		bool RoleExists(string roleName);
		void Save();
	}

	public class RoleFacade : IRoleFacade
	{
		RentlerContext context;

		public RoleFacade()
		{
			this.context = new RentlerContext();
		}

		public void AddRole(string roleName)
		{
			var role = new Role { RoleName = roleName };
			context.Roles.Add(role);
		}

		public void DeleteRole(string roleName)
		{
			var role = (from r in context.Roles
						where r.RoleName == roleName
						select r).SingleOrDefault();

			if (role == null)
				return;

			context.Roles.Remove(role);
		}

		public void AddUsersToRoles(string[] usernames, string[] roleNames)
		{
			var roles = from r in context.Roles
						where roleNames.Contains(r.RoleName)
						select r;

			var users = from u in context.Users
						where !u.IsDeleted &&
						usernames.Contains(u.Username)
						select u;

			foreach (var role in roles)
				foreach (var user in users)
					context.RoleUsers.Add(
						new RoleUser()
						{
							RoleName = role.RoleName,
							UserId = user.UserId
						});
		}

		public string[] FindUsersInRole(string roleName, string username)
		{
			var users = from u in context.Users
						where !u.IsDeleted &&
							  u.RoleUsers.Any(r => r.RoleName == roleName) &&
							  u.Username.Contains(username)
						select u.Username;

			return users.ToArray();
		}

		public string[] GetRoles()
		{
			var roles = from r in context.Roles
						select r.RoleName;

			return roles.ToArray();
		}

		public string[] GetRolesForUser(string username)
		{
			var roles = from r in context.RoleUsers
						where r.User.Username == username
						select r.RoleName;

			return roles.ToArray();
		}

		public string[] GetUsersInRole(string roleName)
		{
			var users = from r in context.RoleUsers
						where r.RoleName == roleName
						select r.User.Username;

			return users.ToArray();
		}

		public bool IsUserInRole(string username, string roleName)
		{
			var isInRole = (from r in context.RoleUsers
							where r.User.Username == username &&
								  r.RoleName == roleName
							select true).Any();

			return isInRole;
		}

		public void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
		{
			var userIds = (from u in context.Users
						   where !u.IsDeleted &&
						   usernames.Contains(u.Username)
						   select u.UserId).ToList();

			var roleUsers = (from u in context.RoleUsers
							 where userIds.Contains(u.UserId) &&
								   roleNames.Contains(u.RoleName)
							 select u).ToList();

			foreach (var item in roleUsers)
				context.RoleUsers.Remove(item);
		}

		public bool RoleExists(string roleName)
		{
			var roleExists = (from r in context.Roles
							  where r.RoleName == roleName
							  select true).Any();

			return roleExists;
		}

		public bool AnyUsersInRole(string roleName)
		{
			var any = (from r in context.RoleUsers
					   where r.RoleName == roleName
					   select true).Any();

			return any;
		}

		public void Save()
		{
			context.SaveChanges();
		}
	}
}
