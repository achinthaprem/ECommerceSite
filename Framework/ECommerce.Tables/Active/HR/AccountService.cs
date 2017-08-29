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

		public Task<Account> GetAccountAsync(string Email)
		{
			Task<Account>                      result              = Task.Run(() =>
			{
				return Account.ExecuteCreateByEmail(Email);
			});

			return result;
		}

		public Task<Account> GetAccountAsync(int ID)
		{
			Task<Account>                      result              = Task.Run(() =>
			{
				return Account.ExecuteCreate(ID);
			});

			return result;
		}

		public Task<bool> ValidatePasswordAsync(int ID, string Password)
		{
			Task<bool>                      result              = Task.Run(() =>
			{
				Account                     account             = Account.ExecuteCreate(ID);
				Password                                        = AccountPasswordHasher.HashPassword(Password, account.Salt);

				return account.Password.Equals(Password);
			});

			return result;
		}

		public Task<bool> ChangePasswordAsync(int ID, string Password, int ModifyingAccountID)
		{
			Task<bool>                      result              = Task.Run(() =>
			{
				bool                        _result             = false;

				string                      salt                = String.Empty;
				string                      hPassword           = AccountPasswordHasher.CreatePassword(Password, out salt);

				Account                     account             = Account.ExecuteCreate(ID);
				account.UpdatePasswordSalt(hPassword, salt, ModifyingAccountID);

				_result                                         = true;

				return _result;
			});

			return result;
		}

		public Task<bool> CheckEmailAsync(string Email)
		{
			Task<bool>                      result              = Task.Run(() =>
			{
				bool                        _result             = false;

				if (Account.ExecuteCreateByEmail(Email) == null)
				{
					_result                                     = true;
				}

				return _result;
			});

			return result;
		}

		public Task<bool> CheckEmailAsync(int ID, string Email)
		{
			Task<bool>                      result              = Task.Run(() =>
			{
				bool                        _result             = false;

				Account                     accByID             = Account.ExecuteCreate(ID);
				Account                     accByEmail          = Account.ExecuteCreateByEmail(Email);

				if (accByEmail == null || (accByID.Email == accByEmail.Email))
				{
					_result                                     = true;
				}
				else
				{
					_result                                     = false;
				}

				return _result;
			});

			return result;
		}

		public Task<bool> CreateAccountAsync(
			string FirstName,
			string LastName,
			string Email,
			string Password,
			string ContactNo,
			string ShippingAddress,
			string Country,
			int Role,
			int CreatingAccountID)
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
																						CreatingAccountID,
																						CreatingAccountID);

				newAccount.Insert();
				
				return (newAccount.ID != -1) ? true : false;
			});
			
			return result;
		}

		public Task<bool> UpdateAccountAsync(
			int ID,
			string FirstName,
			string LastName,
			string Email,
			string ContactNo,
			string ShippingAddress,
			string Country,
			int Status,
			int Role,
			int ModifyingAccountID)
		{
			Task<bool>                      result              = Task.Run(() =>
			{
				bool                        _result             = false;
				Account                     account             = Account.ExecuteCreate(ID);

				if (account != null)
				{
					account.Update(FirstName, LastName, Email, ContactNo, ShippingAddress, Country, Status, Role, ModifyingAccountID);
					_result                                     = true;
				}

				return _result;
			});

			return result;
		}

		public Task<SignInStatus> SignInUserAsync(Account account, string Password)
		{
			Task<SignInStatus>              result              = Task.Run(() =>
			{
				SignInStatus                _result             = SignInStatus.Failure;

				if (account != null)
				{
					if (account.Status == Account.STATUS_ACTIVE)
					{
						if (account.Password.Equals(AccountPasswordHasher.HashPassword(Password, account.Salt)))
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
