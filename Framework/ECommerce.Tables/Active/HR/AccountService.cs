using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;

namespace ECommerce.Tables.Active.HR
{
	public class AccountService : IUserStore<Account>, IUserPasswordStore<Account>
	{
		#region Constructor

		public AccountService()
		{

		}

		#endregion

		#region IUserStore<Account,string> Members

		public Task CreateAsync(Account user)
		{
			user.Insert();

			return Task.Factory.StartNew(() => this.SetPasswordHashAsync(user, user.Password));
		}

		public Task DeleteAsync(Account user)
		{
			throw new NotImplementedException();
		}

		public Task<Account> FindByIdAsync(string userId)
		{
			int                 id          = Convert.ToInt32(userId);
			Task<Account>       user        = Task.Factory.StartNew(() => Account.ExecuteCreate(Convert.ToInt32(userId)));

			return user;
		}

		public Task<Account> FindByNameAsync(string userName)
		{
			Task<Account>       user        = Task.Factory.StartNew(() => Account.ExecuteCreate(userName));
			return user;
		}

		public Task UpdateAsync(Account user)
		{
			return Task.Factory.StartNew(() => user.Update());
		}

		#endregion

		#region IDisposable Members

		public void Dispose() { }

		#endregion

		#region IUserPasswordStore<Account,string> Members

		public Task<string> GetPasswordHashAsync(Account user)
		{
			if (user == null)
			{
				throw new ArgumentNullException("user");
			}

			return Task.FromResult(AccountPasswordHasher.GetFullPassword(user));
		}

		public Task<bool> HasPasswordAsync(Account user)
		{
			bool                result              = false;

			if (!String.IsNullOrEmpty(user.Password))
			{
				result                              = true;
			}

			return Task.FromResult(result);
		}

		public Task SetPasswordHashAsync(Account user, string passwordHash)
		{
			//Create a new hashed password 
			string          fullhashedPassword          = AccountPasswordHasher.CreateHash(passwordHash);

			// Extract the parameters from the hash
			char[]          delimiter                   = { AccountPasswordHasher.SEPARATOR_DELIMETER };
			string[]        split                       = fullhashedPassword.Split(delimiter);

			string          salt                        = split[AccountPasswordHasher.SALT_INDEX];
			string          password                    = split[AccountPasswordHasher.PBKDF2_INDEX];


			//update current record  
			user.UpdatePassword(password, salt);

			return Task.FromResult(0);
		}
		#endregion
	}
}
