using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace ECommerce.Tables.Active.HR
{
	public static class AccountPasswordHasher
	{
		#region Constants

		public const int                ITERATION_COUNT                 = 10000;
		public const KeyDerivationPrf   PRF                             = KeyDerivationPrf.HMACSHA1;
		public const int                NUM_BYTES_REQUESTED             = 256 / 8;
		public const int                SALT_BYTE_LENGTH                = 128 / 8;

		#endregion

		#region Public Methods for Hashing
		/// <summary>
		/// Use this methods to hash user entered password with salt from database
		/// </summary>
		/// <param name="password">User entered password</param>
		/// <param name="_salt">Salt from database</param>
		/// <returns>Hashed Password</returns>
		public static string HashPassword(string password, string _salt)
		{
			byte[]              salt                    = ConvertSalt(_salt);
			string              result                  = Convert.ToBase64String(KeyDerivation.Pbkdf2(
																						password: password,
																						salt: salt,
																						prf: PRF,
																						iterationCount: ITERATION_COUNT,
																						numBytesRequested: NUM_BYTES_REQUESTED));

			return result;
		}

		/// <summary>
		/// Use this method to create a new hashed password to save in database
		/// </summary>
		/// <param name="password">User Entered Password</param>
		/// <param name="_salt">New salt to be stored in database</param>
		/// <returns>Hashed Password</returns>
		public static string CreatePassword(string password, out string _salt)
		{
			string              result                  = null;
			byte[]              salt                    = GenerateSalt();
			_salt                                       = ConvertSalt(salt);

			result                                      = Convert.ToBase64String(KeyDerivation.Pbkdf2(
																						password: password,
																						salt: salt,
																						prf: PRF,
																						iterationCount: ITERATION_COUNT,
																						numBytesRequested: NUM_BYTES_REQUESTED));

			return result;
		}
		#endregion

		#region Internal Methods
		private static string ConvertSalt(byte[] _salt)
		{
			string              result                  = Convert.ToBase64String(_salt);
			return result;
		}

		private static byte[] ConvertSalt(string _salt)
		{
			byte[]              result                  = Convert.FromBase64String(_salt);
			return result;
		}

		private static byte[] GenerateSalt()
		{
			byte[]              result                  = new byte[SALT_BYTE_LENGTH];
			using (var rng = RandomNumberGenerator.Create())
			{
				rng.GetBytes(result);
			}

			return result;
		}
		#endregion
	}
}
