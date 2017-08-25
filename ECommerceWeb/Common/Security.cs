using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace ECommerceWeb.Common
{
	public static class Security
	{
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
																						prf: Constants.PRF,
																						iterationCount: Constants.ITERATION_COUNT,
																						numBytesRequested: Constants.NUM_BYTES_REQUESTED));

			return result;
		}

		/// <summary>
		/// Use this method to hash new password and store in database
		/// </summary>
		/// <param name="password">User Entered Password</param>
		/// <param name="_salt">New salt to be stored in database</param>
		/// <returns>Hashed Password</returns>
		public static string HashPassword(string password, out string _salt)
		{
			string              result                  = null;
			byte[]              salt                    = GenerateSalt();
			_salt                                       = ConvertSalt(salt);

			result                                      = Convert.ToBase64String(KeyDerivation.Pbkdf2(
																						password: password,
																						salt: salt,
																						prf: Constants.PRF,
																						iterationCount: Constants.ITERATION_COUNT,
																						numBytesRequested: Constants.NUM_BYTES_REQUESTED));

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
			byte[]              result                  = new byte[128 / 8];
			using (var rng = RandomNumberGenerator.Create())
			{
				rng.GetBytes(result);
			}

			return result;
		}
		#endregion
	}
}