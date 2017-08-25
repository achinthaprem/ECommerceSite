using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace ECommerceWeb.Common
{
	public class Constants
	{
		// Session
		public const string             USER_ID							= "UserID";
		public const string             USER_NAME						= "UserName";
		public const string             USER_ROLE						= "UserRole";

		// Account
		public const int				ACCOUNT_ACTIVE					= 1;
		public const int				ACCOUNT_INACTIVE				= 0;

		// Actions
		public const string             ACTION_LOGIN                    = "Login";
		public const string             ACTION_LOGOUT                   = "Logout";
		public const string             ACTION_INDEX                    = "Index";
		public const string             ACTION_REGISTER                 = "Register";

		// Controllers
		public const string             CONTROLLER_HOME                 = "Home";
		public const string             CONTROLLER_ACCOUNT              = "Account";

		// Cryptography
		public const int                ITERATION_COUNT                 = 10000;
		public const KeyDerivationPrf   PRF                             = KeyDerivationPrf.HMACSHA1;
		public const int                NUM_BYTES_REQUESTED             = 256 / 8;
		
		// DB Lengths
		public const int                DB_LENGTH_FNAME                 = 50;
		public const int                DB_LENGTH_LNAME                 = 50;
		public const int                DB_LENGTH_EMAIL                 = 255;
		public const int                DB_LENGTH_PASSWORD				= 50;
		public const int                DB_LENGTH_SALT                  = 50;
		public const int                DB_LENGTH_CONTACT_NO            = 30;
		public const int                DB_LENGTH_SHIP_ADD              = 250;
		public const int                DB_LENGTH_COUNTRY               = 50;

		public const int                DB_LENGTH_NAME					= 200;
		public const int                DB_LENGTH_DESCRIPTION           = 1000000000;
		public const int                DB_LENGTH_IMG_NAME              = 500;
		public const int                DB_LENGTH_DECIMAL_MAX           = 16;
		public const int                DB_LENGTH_DECIMAL_DP			= 2;


	}
}
