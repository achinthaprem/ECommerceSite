
namespace ECommerceWeb.Common
{
	public class Constants
	{
		// Session
		public const string             USER_ID							= "UserID";
		public const string             USER_NAME						= "UserName";
		public const string             USER_ROLE						= "UserRole";

		// Login
		public const string             LOGIN_FAIL_USR_PSW              = "Invalid user credentials!";

		// Actions
		public const string             ACTION_LOGIN                    = "Login";
		public const string             ACTION_LOGOUT                   = "Logout";
		public const string             ACTION_INDEX                    = "Index";
		public const string             ACTION_REGISTER                 = "Register";

		// Controllers
		public const string             CONTROLLER_HOME                 = "Home";
		public const string             CONTROLLER_ACCOUNT              = "Account";
		
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
