using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Tables.Active.HR
{
	public class AccountService
	{
		public const int					SELF_REGISTERED     = -1;

		public AccountService()
		{
		}

		public Task<bool> CreateAccount(string FirstName,
			string LastName,
			string Email,
			string Password,
			string ContactNo,
			string ShippingAddress,
			string Country,
			int Role,
			int CreatedAccountID)
		{
			Task<bool>						result				= Task.Run(() =>
			{
				string                      salt                = String.Empty;
				string                      hPassword           = AccountPasswordHasher.CreatePassword(Password, out salt);

				Account                     newAccount          = Account.ExecuteCreate(FirstName, 
																						LastName, 
																						Email, 
																						hPassword, 
																						salt, 
																						ContactNo, 
																						ShippingAddress, 
																						Country, 
																						Account.STATUS_ACTIVE, 
																						(int)Account.RoleCode.All,
																						CreatedAccountID,
																						CreatedAccountID);

				newAccount.Insert();
				
				return (newAccount.ID != -1) ? true : false;
			});
			
			return result;
		}

		public Task<SignInStatus> SignInUser(Account user, string psw)
		{
			Task<SignInStatus>              result              = Task.Run(() =>
			{
				SignInStatus                _result             = SignInStatus.Failure;

				if (user != null)
				{
					if (user.Status == Account.STATUS_ACTIVE)
					{
						if (user.Password.Equals(AccountPasswordHasher.HashPassword(psw, user.Salt)))
						{
							_result                             = SignInStatus.Success;
						}
						else
						{
							_result                             = SignInStatus.Failure;
						}
					}
					else
					{
						_result                                 = SignInStatus.Deactivated;
					}
				}
				else
				{
					_result                                     = SignInStatus.Failure;
				}

				return _result;
			});
			
			return result;
		}
	}

	public enum SignInStatus
	{
		//
		// Summary:
		//     Sign in was successful
		Success = 0,
		//
		// Summary:
		//     Account is deactivated
		Deactivated = 1,
		//
		// Summary:
		//     Sign in failed
		Failure = 2
	}
}
