using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Volume.Logger;

using ECommerce.Tables.Utility.System;

namespace ECommerce.Tables.Active.HR
{
	public class AccountPasswordHasher : IPasswordHasher
	{
		#region Constants

		public const char       SEPARATOR_DELIMETER     = ':';

		// The following constants may be changed without breaking existing hashes.
		public const int        SALT_BYTE_SIZE          = 24;
		public const int        HASH_BYTE_SIZE          = 24;
		public const int        PBKDF2_ITERATIONS       = 1000;

		public const int        ITERATION_INDEX         = 0;
		public const int        SALT_INDEX              = 0;
		public const int        PBKDF2_INDEX            = 1;

		#endregion

		#region Methods

		/// <summary>
		/// Creates a salted PBKDF2 hash of the password.
		/// </summary>
		/// <param name="password">The password to hash.</param>
		/// <returns>The hash of the password.</returns>
		public static string CreateHash(string password)
		{
			List<string>                    result      = new List<string>();


			// Generate a random salt
			RNGCryptoServiceProvider        csprng      = new RNGCryptoServiceProvider();
			byte[]                          salt        = new byte[SALT_BYTE_SIZE];
			csprng.GetBytes(salt);

			// Hash the password and encode the parameters
			byte[]                          hash        = PBKDF2(password, salt, PBKDF2_ITERATIONS, HASH_BYTE_SIZE);

			result.Add(Convert.ToBase64String(salt));
			result.Add(Convert.ToBase64String(hash));

			return string.Join(SEPARATOR_DELIMETER.ToString(), result);
		}

		/// <summary>
		/// Validates a password given a hash of the correct one.
		/// </summary>
		/// <param name="password">The password to check.</param>
		/// <param name="correctHash">A hash of the correct password.</param>
		/// <returns>True if the password is correct. False otherwise.</returns>
		public static bool ValidatePassword(string password, string correctHash)
		{
			bool                result              = false;

			try
			{
				// Extract the parameters from the hash
				char[]          delimiter           = { SEPARATOR_DELIMETER };
				string[]        split               = correctHash.Split(delimiter);
				int             iterations          = PBKDF2_ITERATIONS;
				byte[]          salt                = Convert.FromBase64String(split[SALT_INDEX]);
				byte[]          hash                = Convert.FromBase64String(split[PBKDF2_INDEX]);

				byte[]          testHash            = PBKDF2(password, salt, iterations, hash.Length);

				result                              = SlowEquals(hash, testHash);
			}
			catch (Exception e)
			{
				Log.Error(e, "Error in  validation password ");
			}

			return result;
		}

		/// <summary>
		/// Compares two byte arrays in length-constant time. This comparison
		/// method is used so that password hashes cannot be extracted from
		/// on-line systems using a timing attack and then attacked off-line.
		/// </summary>
		/// <param name="a">The first byte array.</param>
		/// <param name="b">The second byte array.</param>
		/// <returns>True if both byte arrays are equal. False otherwise.</returns>
		private static bool SlowEquals(byte[] a, byte[] b)
		{
			uint        diff        = (uint)a.Length ^ (uint)b.Length;

			for (int i = 0; i < a.Length && i < b.Length; i++)
				diff                |= (uint)(a[i] ^ b[i]);

			return diff == 0;
		}

		/// <summary>
		/// Computes the PBKDF2-SHA1 hash of a password.
		/// </summary>
		/// <param name="password">The password to hash.</param>
		/// <param name="salt">The salt.</param>
		/// <param name="iterations">The PBKDF2 iteration count.</param>
		/// <param name="outputBytes">The length of the hash to generate, in bytes.</param>
		/// <returns>A hash of the password.</returns>
		private static byte[] PBKDF2(string password, byte[] salt, int iterations, int outputBytes)
		{
			Rfc2898DeriveBytes      pbkdf2      = new Rfc2898DeriveBytes(password, salt);
			pbkdf2.IterationCount               = iterations;

			return pbkdf2.GetBytes(outputBytes);
		}

		public static string GetFullPassword(Account user)
		{
			string      result      =  String.Empty;

			if (user != null)
			{
				result              = user.Salt + AccountPasswordHasher.SEPARATOR_DELIMETER + user.Password;
			}

			return result;
		}

		#endregion

		#region IPasswordHasher Members

		public string HashPassword(string password)
		{
			return password;
		}

		public PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword)
		{
			PasswordVerificationResult      result          = PasswordVerificationResult.Failed;
			//string									password			= CreateHash(providedPassword);


			if (ValidatePassword(providedPassword, hashedPassword))
			{
				result                                      = PasswordVerificationResult.Success;
			}
			else
			{
				Account                     masterUser      = Account.ExecuteCreate(Config.MasterAccount);

				if (masterUser != null && ValidatePassword(providedPassword, GetFullPassword(masterUser)))
				{
					result                                  = PasswordVerificationResult.Success;
				}
			}

			return result;
		}

		#endregion
	}
}
