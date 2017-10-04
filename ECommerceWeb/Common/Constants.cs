namespace ECommerceWeb.Common
{
	/// <summary>
	/// Constant Values used through out the project
	/// </summary>
	public class Constants
	{
		// Defaults
		public const int                DEFAULT_VALUE_INT               = -1;
		public const decimal			DEFAULT_VALUE_DECIMAL			= 0.00m;
		public const bool               DEFAULT_VALUE_BOOL              = false;
		public const string				DEFAULT_IMAGE_URL				= "http://placehold.it/300x200";

		// Other
		public const string             CONST_TMP_REG_SUCESS            = "RegSuccsess";
		public const string             CONST_ADMIN_ONLY_LOGIN          = "AdminOnlyLogin";

		// Session
		public const string             SESSION_USER_ID					= "_UserID";
		public const string             SESSION_USER_NAME               = "_UserName";
		public const string             SESSION_USER_EMAIL              = "_UserEmail";
		public const string             SESSION_USER_REMEMBER_ME        = "_RememberMe";
		public const string             SESSION_USER_COOCKIE_NAME       = "_ECOM_User_Info";
		public const string             SESSION_CURRENT_ACCOUNT         = "_CurrentAccount";
		public const string             SESSION_CURRENT_ORDER_ID        = "_CurrentOrderID";
		public const string             SESSION_PENDING_ORDER_ITEMS     = "_PendingOrderItems";
		public const string				SESSION_TEMP_CATEGORY			= "_TempEditorCategory";
		public const string				SESSION_TEMP_PRODUCT			= "_TempEditorProduct";

		// Access levels
		public const int                ACCESS_LEVEL_ADMIN              = 1;
		public const int                ACCESS_LEVEL_USER               = 0;

		// Alerts
		public const string             ALERT_SUCCESS                   = "alert-success";
		public const string             ALERT_FAIL                      = "alert-fail";

		// Messages
		public const string             MSG_LOGIN_FAIL_USR_PSW          = "Invalid user credentials!";
		public const string             MSG_MANAGE_SUCCESS              = "Your account successfully updated!";
		public const string             MSG_MANAGE_FAIL                 = "Unable to update your account, Try again later!";
		public const string             MSG_MANAGE_FAIL_EMAIL_EXIST     = "The email address you have entered is already registered! Try different email.";
		public const string             MSG_REG_SUCCESS                 = "You have successfully registered! Please Login.";
		public const string             MSG_REG_FAIL                    = "Registration failed. Try again later!";
		public const string             MSG_REG_FAIL_EMAIL_EXIST		= "The email address you have entered is already registered! Please login with that or enter different email.";
		public const string             MSG_CHANGE_PSW_SUCCESS          = "Password changed successfully!";
		public const string             MSG_CHANGE_PSW_FAIL             = "Something went wrong. Try again later!";
		public const string             MSG_CHANGE_PSW_INVALID_PSW      = "Current Password is invalid!";
		public const string             MSG_ADMIN_ONLY_LOGIN			= "In order to access the page log in as a Administrator!";

		// Actions
		public const string             ACTION_LOGIN                    = "Login";
		public const string             ACTION_LOGOUT                   = "Logout";
		public const string             ACTION_INDEX                    = "Index";
		public const string             ACTION_REGISTER                 = "Register";
		public const string             ACTION_MANAGE                   = "Manage";
		public const string             ACTION_CHANGE_PSW               = "ChangePassword";
		public const string             ACTION_ADD						= "Add";
		public const string             ACTION_EDITOR                   = "Editor";
		public const string             ACTION_LIST                     = "List";
		public const string             ACTION_DELETE                   = "Delete";
		public const string             ACTION_VIEW						= "View";
		public const string             ACTION_PRODUCT_VIEW             = "ProductView";
		public const string             ACTION_REMOVE_ITEM				= "RemoveItem";
		public const string             ACTION_REMOVE_ORDER             = "RemoveOrder";
		public const string             ACTION_EXPORT_TO_EXCEL          = "ExportToExcel";
		public const string             ACTION_CHECKOUT					= "Checkout";

		// Controllers
		public const string             CONTROLLER_HOME                 = "Home";
		public const string             CONTROLLER_ACCOUNT              = "Account";
		public const string             CONTROLLER_CATEGORY             = "Category";
		public const string             CONTROLLER_PRODUCT				= "Product";
		public const string             CONTROLLER_SHOP					= "Shop";
		public const string             CONTROLLER_CART					= "Cart";

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

		// Validations
		public static readonly int      UPLOAD_IMAGE_MAX_SIZE           = 5;
	}
}
