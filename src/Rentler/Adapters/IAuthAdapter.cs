using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rentler.Data;
using Rentler.Redis;
using Rentler.Configuration;
using System.IO;
using ProtoBuf;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Rentler.Adapters
{
	public interface IAuthAdapter
	{
		Status<ApiKey> ValidateApiKey(Guid? key);

		Status<Guid> GetAuthToken(ApiKey apiKey, string affiliateUserKey,
			string email, string passwordHash,
			string firstName, string lastName, string username);

		Status<User> ValidateAuthToken(Guid token);
	}

    public class AuthAdapter : IAuthAdapter
    {
        IAccountAdapter accountAdapter;

        public AuthAdapter(IAccountAdapter accountAdapter)
        {
            this.accountAdapter = accountAdapter;
        }

        public Status<ApiKey> ValidateApiKey(Guid? key)
        {
            if (!key.HasValue)
                return Status.UnAuthorized<ApiKey>();

            ApiKey apiKey = GetApiKey(key.Value);

            if (apiKey != null)
                return Status.OK<ApiKey>(apiKey);

            return Status.UnAuthorized<ApiKey>();
        }

        ApiKey GetApiKey(Guid key)
        {
            ApiKey apiKey = null;

            //L1
            var cache = HttpContext.Current.Cache;
            apiKey = cache[CacheKeys.API_KEYS] as ApiKey;

            if (apiKey != null)
                return apiKey;

            //L2
            var redisConnection = ConnectionGateway.Current.GetReadConnection();
			
			var task = redisConnection.Hashes.Get(App.RedisDatabase, CacheKeys.API_KEYS, key.ToString());
			var result = redisConnection.Wait(task);

			if (result != null)
			{
				using (var stream = new MemoryStream(result))
					apiKey = Serializer.Deserialize<ApiKey>(stream);

				return apiKey;
			}
			

            //L3
            using (var context = new RentlerContext())
                apiKey = (from k in context.ApiKeys
                          where k.ApiKeyId == key
                          select k).SingleOrDefault();

            if (apiKey != null)
            {
                //store in L2
				var connection = ConnectionGateway.Current.GetWriteConnection();
                var storeTask = connection.Hashes.Set(App.RedisDatabase,
                    CacheKeys.API_KEYS, apiKey.ApiKeyId.ToString(), apiKey.ToBinaryArray());

                //store in L1
                cache.Add(CacheKeys.API_KEYS,
                    apiKey, null,
                    DateTime.Now.AddMinutes(5),
                    System.Web.Caching.Cache.NoSlidingExpiration,
                    System.Web.Caching.CacheItemPriority.Normal, null);
            }

            return apiKey;
        }

        public Status<Guid> GetAuthToken(ApiKey apiKey, string affiliateUserKey,
            string email, string passwordHash,
            string firstName, string lastName, string username)
        {
			RedisPublisher.Publish("token","Getting token for api key: " + apiKey.ToString());

            //check if user exists
            var userId = accountAdapter.GetAffiliateUserIdByUsernameOrEmailAndApiKey(email, apiKey.ApiKeyId);

            //did we not find anything?
            if (userId.StatusCode == 404)
            {
                //try again, looking for regular rentler users this time
                userId = accountAdapter.GetUserIdByUsernameOrEmail(email);

                //still didn't find anything?
                if (userId.StatusCode == 404)
                {
                    //we've got a new one, make 'em
                    var user = new User();
                    user.Email = email;
                    user.FirstName = firstName;
                    user.LastName = lastName ?? string.Empty;
                    user.Username = username;
                    user.PasswordHash = string.Empty;
                    user.CreateDateUtc = DateTime.UtcNow;
                    user.UpdatedBy = "ksl auth";
                    user.UpdateDateUtc = DateTime.UtcNow;

                    var status = Status.Validatate<User>(user);

                    if (status.StatusCode != 200)
                    {
                        var result = Status.NotFound<Guid>();
                        result.Errors = status.Errors;

                        return result;
                    }


                    using (var context = new RentlerContext())
                    {
                        context.Users.Add(user);
                        context.SaveChanges();
                        userId = Status.OK(user.UserId);
                    }
                }

                //Okay, now they're either already a Rentler user but are coming in from an affiliate, 
                //or they just became one from an affiliate, so create an affiliate user for them.
                using (var context = new RentlerContext())
                {
                    var affiliate = new AffiliateUser();
                    affiliate.UserId = userId.Result;
                    affiliate.AffiliateUserKey = affiliateUserKey;
                    affiliate.ApiKey = apiKey.ApiKeyId;
                    affiliate.PasswordHash = passwordHash;
                    context.AffiliateUsers.Add(affiliate);
                    context.SaveChanges();
                }
            }

            //generate auth token
            var token = CreateAuthToken(userId.Result);

			RedisPublisher.Publish("token", "Returning token: " + token.ToString());

            //return token
            return Status.OK(token);
        }

        Guid CreateAuthToken(int userId)
        {
            var token = Guid.NewGuid();

            //store in L2
			RedisPublisher.Publish("token","Creating token for userId: " + userId );

			var connection = ConnectionGateway.Current.GetWriteConnection();
			var task = connection.Hashes.Set(App.RedisDatabase, CacheKeys.AUTH_TOKENS, token.ToString(), userId.ToString());
            connection.Wait(task);

            //store in L1
            var cache = HttpContext.Current.Cache;
            cache[CacheKeys.AUTH_TOKENS + ":" + token.ToString()] = userId;

            return token;
        }

        public Status<User> ValidateAuthToken(Guid token)
        {
			RedisPublisher.Publish("token", "Validating token: " + token.ToString());

            //L1
            var cache = HttpContext.Current.Cache;
            int? userId = cache[CacheKeys.AUTH_TOKENS + ":" + token.ToString()] as int?;

			if (!userId.HasValue || userId == 0)
			{
				//L2
                var redisConnection = ConnectionGateway.Current.GetReadConnection();

                var task = redisConnection.Hashes.GetString(App.RedisDatabase, CacheKeys.AUTH_TOKENS, token.ToString());
                var result = redisConnection.Wait(task);

				int userIdTryParse = 0;
				if (int.TryParse(result, out userIdTryParse))
					userId = userIdTryParse;
				
			}

            if (userId.HasValue && userId != 0)
            {
                using (var context = new RentlerContext())
                {
                    var user = (from u in context.Users
                                where u.UserId == userId.Value
                                select u).SingleOrDefault();

                    if (user == null)
                        return Status.NotFound<User>();

                    //otherwise we're good, clear cache and return
                    //L2
					var connection = ConnectionGateway.Current.GetWriteConnection();
                    var task = connection.Hashes.Remove(App.RedisDatabase, CacheKeys.AUTH_TOKENS, token.ToString());

                    //L1
                    cache.Remove(CacheKeys.AUTH_TOKENS + ":" + token.ToString());

                    return Status.OK(user);
                }
            }

            return Status.NotFound<User>();
        }
    }
}
