using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Volume.Logger;
using Volume.Toolkit.Encryption;

using ECommerce.Tables.Utility.System;

namespace ECommerce.Tables.Active.HR
{
	/*http://eliot-jones.com/2014/10/asp-identity-2-0-password-reset*/
	public class AccountTokenProvider<TUser> : IUserTokenProvider<Account, string> where TUser : class, IUser
	{
		public Task<string> GenerateAsync(string purpose, UserManager<Account, string> manager, Account user)
		{
			return Task.FromResult<string>(this.GetEncryptedCode(user.ID));
		}

		public Task<bool> IsValidProviderForUserAsync(UserManager<Account, string> manager, Account user)
		{
			if (manager == null) throw new ArgumentNullException();
			else
			{
				return Task.FromResult<bool>(manager.SupportsUserPassword);
			}
		}

		public Task NotifyAsync(string token, UserManager<Account, string> manager, Account user)
		{
			return Task.FromResult<int>(0);
		}

		public Task<bool> ValidateAsync(string purpose, string token, UserManager<Account, string> manager, Account user)
		{
			return Task.FromResult<bool>(this.IsValidCode(user.ID, token));
		}


		private string GetEncryptedCode(int userID)
		{
			string                  result                  = String.Empty;
			string                  guid                    = Guid.NewGuid().ToString();
			string                  dateTimeValue           = TableDateTimeUtility.GetDateTimeNow().AddMinutes(Config.TokenLifespanMinutes).ToString();
			try
			{
				string              encryptText             = userID + "_" +guid + "_" + dateTimeValue;
				result                                      = Encryption.Encrypte(encryptText);
			}
			catch (Exception ex)
			{
				Log.Warning(ex, "Failed encrypt authentication token with data " + guid + " and time stamp = " + dateTimeValue);
			}

			return result;
		}

		private bool IsValidCode(int userID, string code)
		{
			bool                                result              =  false;

			try
			{
				// Decrypt token
				string                              decodedToken        = Encryption.Decrypte(code);
				string[]                            splitData           = decodedToken.Split('_');

				if (splitData.Length == 3)
				{
					if (userID.ToString() == splitData[0])
					{
						DateTime                    expireDate          = Convert.ToDateTime(splitData[2]);

						if (expireDate > TableDateTimeUtility.GetDateTimeNow())
						{
							result                                  = true;
						}
					}
				}
			}
			catch (Exception e)
			{
				Log.Error(e, "Error in decrypting token :" + code + " for user ID " + userID);
			}

			return result;
		}
	}
}
