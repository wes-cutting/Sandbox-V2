using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rentler.Data;
using System.Web.Security;
using Rentler.Redis;
using Rentler.Configuration;
using System.Web;
using Rentler.Web.Email;
using Rentler.Auth;
using System.Security.Cryptography;

namespace Rentler.Adapters
{
	/// <summary>
	/// Adapter for managing basic account information.
	/// </summary>
	public interface IAccountAdapter
	{
		/// <summary>
		/// Verifies a user for correct login
		/// </summary>
		/// <param name="username">The username.</param>
		/// <param name="password">The password.</param>
		/// <returns>Status result with the working user if successful.</returns>
		Status<User> LoginUser(string username, string password);

		/// <summary>
		/// Registers the user.
		/// </summary>
		/// <param name="user">The user to register.</param>
		/// <param name="password">The password.</param>
		/// <returns>Status result with the newly created user.</returns>
		Status<User> RegisterUser(User user, string password);

		/// <summary>
		/// Updates the profile of the specified user
		/// </summary>
		/// <param name="username">the username of the user</param>
		/// <param name="firstName">first name of user</param>
		/// <param name="lastName">last name of user</param>
		/// <param name="email">email of user</param>
		/// <returns>status result with this updated user</returns>
		Status<User> UpdateProfile(string username, string firstName, string lastName, string email);

		/// <summary>
		/// Changes the password of the specified user
		/// </summary>
		/// <param name="username">the username of the user</param>
		/// <param name="oldPassword">old password for user</param>
		/// <param name="newPassword">new password for user</param>
		/// <param name="confirmPassword">confirmed new password for user</param>
		/// <returns></returns>
		Status<User> ChangePassword(string username, string oldPassword, string newPassword, string confirmPassword);

		/// <summary>
		/// Gets the application for user. If the application doesn't
		/// exist then one is created.
		/// </summary>
		/// <param name="username">The username of the user.</param>
		/// <returns>A user application.</returns>
		Status<UserApplication> GetApplicationForUser(string username);

		/// <summary>
		/// Sets the application for user.
		/// </summary>
		/// <param name="username">The username of the user to set the application for.</param>
		/// <param name="userApplication">The user's application.</param>
		/// <returns>The user application that was saved.</returns>
		Status<UserApplication> SaveApplicationForUser(string username, UserApplication userApplication);

		/// <summary>
		/// Get user by username
		/// </summary>
		/// <param name="username">the username</param>
		/// <returns>Status result with requested user</returns>
		Status<User> GetUser(string username);

		/// <summary>
		/// Get the userId by either their username or email address, and an apiKey
		/// </summary>
		/// <param name="usernameOrEmail">Either the username or the email address of the user</param>
		/// <param name="apiKey">The apiKey</param>
		/// <returns></returns>
		Status<int> GetAffiliateUserIdByUsernameOrEmailAndApiKey(string usernameOrEmail, Guid apiKey);

		/// <summary>
		/// Gets the userId by either their username or email address.
		/// </summary>
		/// <param name="email">Either the username or the email of the user.</param>
		/// <returns>A status object of the id of the user</returns>
		Status<int> GetUserIdByUsernameOrEmail(string usernameOrEmail);

		/// <summary>
		/// Resets the password of the specified user
		/// </summary>        
		/// <param name="email">user's email</param>
		/// <returns></returns>
		Status<User> ResetPassword(string email);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="username"></param>
		/// <param name="userInterestId"></param>
		/// <returns></returns>
		Status<UserInterest> GetUserInterest(string username, int userInterestId);

		/// <summary>
		/// 
		/// </summary>
		/// <param name="username"></param>
		/// <param name="userInterestId"></param>
		/// <param name="application"></param>
		/// <returns></returns>
		Status<UserInterest> SendApplication(string username, int userInterestId, UserApplication application);
	}

	/// <summary>
	/// Concrete adapter for managing account information.
	/// </summary>
	public class AccountAdapter : IAccountAdapter
	{
		private IAccountMailer mailer;
		/// <summary>
		/// Initializes a new instance of the <see cref="AccountAdapter"/> class.
		/// </summary>
		public AccountAdapter(IAccountMailer mailer)
		{
			this.mailer = mailer;
		}

		/// <summary>
		/// Verifies a user for correct login
		/// </summary>
		/// <param name="username">The username.</param>
		/// <param name="password">The password.</param>
		/// <returns>
		/// Status result with the working user if successful.
		/// </returns>
		public Status<User> LoginUser(string username, string password)
		{
			if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
				return Status.ValidationError<User>(null, "UserName", "The username or password is incorrect");

			string hashedPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "SHA1");

			using (var context = new RentlerContext())
			{
                // find rentler user
                var user = (from u in context.Users
                            where u.Email == username ||
                            u.Username == username
                            select u).FirstOrDefault();

                // user not found, need a user record
                if (user == null)
                    return Status.ValidationError<User>(null, "UserName", "The username or password is incorrect");

                // user has rentler password and it matched
                if (user.PasswordHash == hashedPassword)
                    return Status.OK<User>(user);

                // no password match on rentler user
                // don't worry though, could still be an affiliate user
                context.Entry(user).Reference("AffiliateUser").Load();
                                
                // make sure the user has an affiliate user
                // make sure the affiliate user has a password to check
                //"$A" denotes a "Mode A" Ksl Md5 hashing algorithm
			    if( user.AffiliateUser != null &&
                    user.AffiliateUser.PasswordHash != null &&
                    user.AffiliateUser.PasswordHash.StartsWith("$A"))
				    if(CheckKslPassword(password, user.AffiliateUser.PasswordHash))
					    return Status.OK<User>(user);

                return Status.ValidationError<User>(null, "Password", "The username or password is incorrect");
			}
		}

		/// <summary>
		/// Registers the specified user.
		/// </summary>
		/// <param name="user">The user.</param>
		/// <param name="password">The password.</param>
		/// <returns></returns>
		public Status<User> RegisterUser(User user, string password)
		{
			user.CreateDateUtc = DateTime.UtcNow;
			user.UpdateDateUtc = DateTime.UtcNow;
			user.UpdatedBy = "AccountAdapter";

			// validate the object
			var result = Status.Validatate<User>(user);
			if (result.StatusCode != 200)
				return result;

			if (string.IsNullOrWhiteSpace(password))
				return Status.ValidationError<User>(null, "Password", "The password was not specified");

			using (var context = new RentlerContext())
			{
				try
				{                    
					// check for dup email
					var dupEmail = context.Users.Where(u => !u.IsDeleted && u.Email.ToLower() == user.Email.ToLower()).Count();
					if (dupEmail > 0)
					{
						return Status.ValidationError<User>(null, "Email", "The email is already used");
					}

					// check for dup username
					var dupUser = context.Users.Where(u => !u.IsDeleted && u.Username.ToLower() == user.Username.ToLower()).Count();
					if (dupUser > 0)
					{
						return Status.ValidationError<User>(null, "Username", "The username is already used");
					}

					user.PasswordHash = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "SHA1");

					// create the user
					context.Users.Add(user);
					context.SaveChanges();

					// notify user by email that their password was changed successfully.
					EmailAccountRegisterModel model = new EmailAccountRegisterModel()
					{
						Name = string.Format("{0} {1}", user.FirstName, user.LastName),
						To = user.Email
					};
					mailer.Register(model);

					return Status<User>.OK(user);
				}
				catch (Exception ex)
				{
					// TODO: log exception
					return Status.Error<User>("System was unable to create user", null);
				}
			}
		}


		/// <summary>
		/// Updates the profile of the specified user
		/// </summary>
		/// <param name="username">the username of the user</param>
		/// <param name="firstName">first name of user</param>
		/// <param name="lastName">last name of user</param>
		/// <param name="email">email of user</param>
		/// <returns>
		/// status result with this updated user
		/// </returns>
		public Status<User> UpdateProfile(string username, string firstName, string lastName, string email)
		{
			using (var context = new RentlerContext())
			{
				// get user to update
				var userStatus = GetUser(username, context);

				if (userStatus.StatusCode != 200)
					return userStatus;

				var user = userStatus.Result;

				// email changed
				if (user.Email.ToLower() != email.ToLower())
				{
					var dupEmail = context.Users
						.Where(u => !u.IsDeleted && u.Email.ToLower() == email.ToLower())
						.Count();

					if (dupEmail > 0)
						return Status.ValidationError<User>(null, "Email", "The email is already used");

					// ok to update
					user.Email = email;
				}

				user.FirstName = firstName;
				user.LastName = lastName;
				user.UpdatedBy = "AccountAdapter";
				user.UpdateDateUtc = DateTime.UtcNow;

				context.SaveChanges();

				return Status<User>.OK(user);
			}
		}

		/// <summary>
		/// Changes the password of the specified user
		/// </summary>
		/// <param name="username">the username of the user</param>
		/// <param name="oldPassword">old password for user</param>
		/// <param name="newPassword">new password for user</param>
		/// <param name="confirmPassword">confirmed new password for user</param>
		/// <returns></returns>
		public Status<User> ChangePassword(string username, string oldPassword, string newPassword, string confirmPassword)
		{
			if (confirmPassword != newPassword)
				return Status<User>.ValidationError<User>(null, "ConfirmPassword", "Passwords do not match");

			using (var context = new RentlerContext())
			{
				// get user whose password needs to be reset
				var userStatus = GetUser(username, context);

				if (userStatus.StatusCode != 200)
					return userStatus;

				var user = userStatus.Result;

				if (user.PasswordHash != FormsAuthentication.HashPasswordForStoringInConfigFile(oldPassword, "SHA1"))
					return Status<User>.ValidationError<User>(null, "OldPassword", "Old Password is incorrect");

				try
				{
					// reset password
					user.PasswordHash = FormsAuthentication.HashPasswordForStoringInConfigFile(newPassword, "SHA1");
					context.SaveChanges();

					// notify user by email that their password was changed successfully.
					EmailChangePasswordModel model = new EmailChangePasswordModel()
					{
						Name = string.Format("{0} {1}", user.FirstName, user.LastName),
						To = user.Email
					};
					mailer.ChangePassword(model);

					return Status<User>.OK(user);
				}
				catch (Exception ex)
				{
					// TODO: log exception
					return Status.Error<User>("System was unable to change password", null);
				}
			}
		}

		/// <summary>
		/// Get user by username
		/// </summary>
		/// <param name="username">the username</param>
		/// <returns>
		/// Status result with requested user
		/// </returns>
		public Status<User> GetUser(string username)
		{
			using (var context = new RentlerContext())
			{
				return GetUser(username, context);
			}
		}

		/// <summary>
		/// Gets the user privately.
		/// </summary>
		/// <param name="username">The username.</param>
		/// <param name="context">The context for this unit of work.</param>
		/// <returns>A status with the user</returns>
		private Status<User> GetUser(string username, RentlerContext context)
		{
			try
			{
				var user = (from u in context.Users
							where !u.IsDeleted && u.Username == username
							select u).FirstOrDefault();

				if (user == null)
					return Status<User>.NotFound<User>();

				return Status<User>.OK(user);
			}
			catch (Exception ex)
			{
				// TODO: log exception
				return Status.Error<User>("System was unable to find user", null);
			}
		}

		/// <summary>
		/// Resets the password of the specified user
		/// </summary>
		/// <param name="email">user's email</param>
		/// <returns></returns>
		public Status<User> ResetPassword(string email)
		{
			using (var context = new RentlerContext())
			{
				try
				{
					var user = (from u in context.Users
								where !u.IsDeleted && u.Email == email
								select u).FirstOrDefault();

					if (user == null)
						return Status.ValidationError<User>(null, "Email", "No user with this email exists");

					// generate a random password 8 char long
					string newPassword = FormsAuthentication
						.HashPasswordForStoringInConfigFile(
							DateTime.Now.ToString() + user.Username,
							"SHA1")
						.Substring(0, 8);

					// hash the random password and update user with it
					user.PasswordHash = FormsAuthentication
						.HashPasswordForStoringInConfigFile(newPassword, "SHA1");
					context.SaveChanges();

					EmailForgotPasswordModel model = new EmailForgotPasswordModel()
					{
                        To = user.Email,
						Username = user.Username,
						NewPassword = newPassword
					};
					mailer.ForgotPassword(model);

					return Status.OK<User>(user);
				}
				catch (Exception ex)
				{
					// TODO: log exception
					return Status.Error<User>("System was unable to reset password", null);
				}
			}
		}

		/// <summary>
		/// Gets the application for user. If the application doesn't
		/// exist then one is created.
		/// </summary>
		/// <param name="username">The username of the user.</param>
		/// <returns>
		/// A user application.
		/// </returns>
		public Status<UserApplication> GetApplicationForUser(string username)
		{
			using (var context = new RentlerContext())
			{
				// get the user
				User result = (from u in context.Users.Include("UserApplication")
							   where u.Username == username
							   select u).SingleOrDefault();

				if (result == null)
					return Status.NotFound<UserApplication>();

				UserApplication application;

				if (result.UserApplication == null)
				{
					application = new UserApplication();
					application.UserId = result.UserId;
					context.UserApplications.Add(application);
					context.SaveChanges();
				}
				else
				{
					application = result.UserApplication;
				}

				return Status.OK<UserApplication>(application);
			}
		}

		/// <summary>
		/// Sets the application for user.
		/// </summary>
		/// <param name="username">The username of the user to set the application for.</param>
		/// <param name="userApplication">The user's application.</param>
		/// <returns>
		/// The user application that was saved.
		/// </returns>
		public Status<UserApplication> SaveApplicationForUser(
			string username, UserApplication userApplication)
		{
            var identity = CustomAuthentication.GetIdentity();

            if (!identity.IsAuthenticated)
                return Status.UnAuthorized<UserApplication>();

			using (var context = new RentlerContext())
			{
                try
                {
                    bool isNew = false;

                    var application = (from u in context.UserApplications
                                       where u.UserId == identity.UserId
                                       select u).SingleOrDefault();

                    if (application == null)
                    {                        
                        application = new UserApplication { UserId = identity.UserId };
                        isNew = true;
                    }

                    application.ConvictedExplaination = userApplication.ConvictedExplaination;
                    application.EmergencyContact = userApplication.EmergencyContact;
                    application.EmergencyContactAddressLine1 = userApplication.EmergencyContactAddressLine1;
                    application.EmergencyContactAddressLine2 = userApplication.EmergencyContactAddressLine2;
                    application.EmergencyContactCity = userApplication.EmergencyContactCity;
                    application.EmergencyContactPhone = userApplication.EmergencyContactPhone;
                    application.EmergencyContactState = userApplication.EmergencyContactState;
                    application.EmergencyContactZip = userApplication.EmergencyContactZip;
                    application.FirstName = userApplication.FirstName;
                    application.HasBeenConvicted = userApplication.HasBeenConvicted;
                    application.HasEverBeenUnlawfulDetainer = userApplication.HasEverBeenUnlawfulDetainer;
                    application.LastName = userApplication.LastName;
                    application.PresentAddressLine1 = userApplication.PresentAddressLine1;
                    application.PresentAddressLine2 = userApplication.PresentAddressLine2;
                    application.PresentCity = userApplication.PresentCity;
                    application.PresentEmployer = userApplication.PresentEmployer;
                    application.PresentEmployerPhone = userApplication.PresentEmployerPhone;
                    application.PresentLandlord = userApplication.PresentLandlord;
                    application.PresentLandlordPhone = userApplication.PresentLandlordPhone;
                    application.PresentPhone = userApplication.PresentPhone;
                    application.PresentState = userApplication.PresentState;
                    application.PresentZip = userApplication.PresentZip;
                    application.PreviousAddressLine1 = userApplication.PreviousAddressLine1;
                    application.PreviousAddressLine2 = userApplication.PreviousAddressLine2;
                    application.PreviousCity = userApplication.PreviousCity;
                    application.PreviousEmployer = userApplication.PreviousEmployer;
                    application.PreviousEmployerPhone = userApplication.PreviousEmployerPhone;
                    application.PreviousLandlord = userApplication.PreviousLandlord;
                    application.PreviousLandlordPhone = userApplication.PreviousLandlordPhone;
                    application.PreviousState = userApplication.PreviousState;
                    application.PreviousZip = userApplication.PreviousZip;
                    application.Ssn = userApplication.Ssn;
                    application.UpdateDateUtc = DateTime.UtcNow;
                    application.UpdatedBy = "accountadapter";
                    application.VehicleColor = userApplication.VehicleColor;
                    application.VehicleLicenseNumber = userApplication.VehicleLicenseNumber;
                    application.VehicleMake = userApplication.VehicleMake;
                    application.VehicleModel = userApplication.VehicleModel;
                    application.VehicleState = userApplication.VehicleState;
                    application.VehicleYear = userApplication.VehicleYear;

                    // new applications need to be added to the context
                    if (isNew)
                        context.UserApplications.Add(application);
                    
                    context.SaveChanges();

                    return Status.OK<UserApplication>(application);
                }
                catch (Exception ex)
                {
                    // TODO: log exception
                    return Status.Error<UserApplication>("System was unable to create/update application", null);
                }
			}
		}

		/// <summary>
		/// Checks if there is a user with a given email/username
		/// that is associated with that apiKey.
		/// This method uses L1, L2, and L3 caching.
		/// </summary>
		/// <param name="usernameOrEmail">The username/email to check for</param>
		/// <param name="apiKey">An apiKey provided to a 3rd party</param>
		/// <returns>A Status result of the id of the user</returns>
		public Status<int> GetAffiliateUserIdByUsernameOrEmailAndApiKey(string usernameOrEmail, Guid apiKey)
		{
			int? userId;

			//L1
			var cache = HttpContext.Current.Cache;
			userId = cache[CacheKeys.AFFILIATE_USER_IDS + ":" + usernameOrEmail + ":" + apiKey.ToString()] as int?;

			if (userId.HasValue)
				return Status.OK(userId.Value);

			//L2
            var redisConnection = ConnectionGateway.Current.GetReadConnection();
			
			var task = redisConnection.Hashes.GetString(App.RedisDatabase, CacheKeys.AFFILIATE_USER_IDS, usernameOrEmail + ":" + apiKey.ToString());
			var result = redisConnection.Wait(task);
			if (result != null)
			{
				//store in L1
				cache[CacheKeys.AFFILIATE_USER_IDS + ":" + usernameOrEmail + ":" + apiKey.ToString()] = int.Parse(result);

				return Status.OK(int.Parse(result));
			}
			

			//L3
			using (var context = new RentlerContext())
			{
				userId = (from u in context.Users
						  where !u.IsDeleted &&
								(u.Username.Equals(usernameOrEmail) ||
								u.Email.Equals(usernameOrEmail)) &&
								u.AffiliateUser.ApiKey.Equals(apiKey)
						  select u.UserId).FirstOrDefault();

				if (userId.HasValue && userId.Value != 0)
				{
					//store in L2
					var connection = ConnectionGateway.Current.GetWriteConnection();
					var storeTask = connection.Hashes.Set(App.RedisDatabase, CacheKeys.AFFILIATE_USER_IDS, usernameOrEmail + ":" + apiKey.ToString(), userId.Value.ToString());

					//store in L1
					cache[CacheKeys.AFFILIATE_USER_IDS + ":" + usernameOrEmail + ":" + apiKey.ToString()] = userId.Value;

					return Status.OK(userId.Value);
				}

				return Status.NotFound<int>();
			}
		}

		public Status<int> GetUserIdByUsernameOrEmail(string usernameOrEmail)
		{
			int? userId;

			//L1
			var cache = HttpContext.Current.Cache;
			userId = cache[CacheKeys.USER_IDS + ":" + usernameOrEmail] as int?;

			if (userId.HasValue)
				return Status.OK(userId.Value);

			//L2
            var redisConnection = ConnectionGateway.Current.GetReadConnection();

            var task = redisConnection.Hashes.GetString(App.RedisDatabase, CacheKeys.USER_IDS, usernameOrEmail);
            var result = redisConnection.Wait(task);
			if (result != null)
			{
				//store in L1
				cache[CacheKeys.USER_IDS + ":" + usernameOrEmail] = int.Parse(result);

				return Status.OK(int.Parse(result));
			}
			

			//L3
			using (var context = new RentlerContext())
			{
				userId = (from u in context.Users
						  where !u.IsDeleted &&
								(u.Username.Equals(usernameOrEmail) ||
								u.Email.Equals(usernameOrEmail))
						  select u.UserId).FirstOrDefault();

				if (userId.HasValue && userId.Value != 0)
				{
					//store in L2
					var connection = ConnectionGateway.Current.GetWriteConnection();
					var storeTask = connection.Hashes.Set(App.RedisDatabase, CacheKeys.USER_IDS, usernameOrEmail, userId.Value.ToString());

					//store in L1
					cache[CacheKeys.USER_IDS + ":" + usernameOrEmail] = userId.Value;

					return Status.OK(userId.Value);
				}

				return Status.NotFound<int>();
			}
		}

		public Status<UserInterest> GetUserInterest(string username, int userInterestId)
		{
			if (string.IsNullOrWhiteSpace(username))
				return Status.ValidationError<UserInterest>(null, "username", "The username is required");

			using (RentlerContext context = new RentlerContext())
			{
				try
				{
					var lead = (from i in context.UserInterests.Include("User.UserApplication").Include("Building.User")
								where i.User.Username == username && i.UserInterestId == userInterestId
								select i).SingleOrDefault();

					if (lead == null)
						return Status.NotFound<UserInterest>();

					return Status.OK(lead);
				}
				catch (Exception ex)
				{
					// log exception
					return Status.Error<UserInterest>("System was unable to get lead", null);
				}
			}
		}

		public Status<UserInterest> SendApplication(string username, int userInterestId, UserApplication application)
		{
			if (string.IsNullOrWhiteSpace(username))
				return Status.ValidationError<UserInterest>(null, "username", "The username is required");

			// validate UserApplication
			var appStatusValid = Status.Validatate<UserApplication>(application);

			if (appStatusValid.StatusCode != 200)
				return Status.ValidationError<UserInterest>(null, "application", "The application is not valid");

			// update user application
			var appStatusSave = SaveApplicationForUser(username, application);

			if (appStatusSave.StatusCode != 200)
				return Status.Error<UserInterest>("System was unable to update application", null);

			using (RentlerContext context = new RentlerContext())
			{
				try
				{
					// get lead
					var lead = (from i in context.UserInterests
								where i.User.Username == username && i.UserInterestId == userInterestId
								select i).SingleOrDefault();

					if (lead == null)
						return Status.NotFound<UserInterest>();

					// update lead - ApplicationSubmitted = true
					lead.ApplicationSubmitted = true;

					context.SaveChanges();

					EmailSendApplicationModel model = new EmailSendApplicationModel(lead);
					mailer.SendApplication(model);

					return Status.OK(lead);
				}
				catch (Exception ex)
				{
					// log exception
					return Status.Error<UserInterest>("System was unable to get lead", null);
				}
			}
		}

        private static bool CheckKslPassword(string password, string hashedPassword)
        {
            string[] split = hashedPassword.Split('$');
            var ksl = new
            {
                Mode = split[1],
                Salt = split[2],
                Hash = split[3]
            };

            //Salt and hash the password 1000 times.
            var hashed = password;
            for (int i = 0; i < 1000; i++)
                hashed = PHPMD5Hash(hashed + ksl.Salt);

            //format the hash to KSL's specs
            //Ksl's Format: ${Mode}${Salt}${Hash}
            //i.e: $A$8AATG4$b74e4aa0d836401dddd71e265f929aab
            var kslHashed = string.Format("${0}${1}${2}", ksl.Mode, ksl.Salt, hashed);

            //are they equal?
            return kslHashed.Equals(hashedPassword);
        }

        /// <summary>
        /// MD5 Hashing scheme that is compatible with PHP's default methods.
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        private static string PHPMD5Hash(string password)
        {
            byte[] asciiBytes = ASCIIEncoding.ASCII.GetBytes(password);
            byte[] hashedBytes = MD5CryptoServiceProvider.Create().ComputeHash(asciiBytes);
            string hashedString = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();

            return hashedString;
        }
    }
}
