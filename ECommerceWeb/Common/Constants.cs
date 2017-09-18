
namespace ECommerceWeb.Common
{
	public class Constants
	{
		// Other
		public const string             CONST_TMP_REG_SUCESS            = "RegSuccsess";
		public const string             CONST_ADMIN_ONLY_LOGIN          = "AdminOnlyLogin";

		// Session
		public const string             USER_ID							= "UserID";
		public const string             USER_NAME                       = "UserName";
		public const string             USER_EMAIL                      = "UserEmail";
		public const string             USER_REMEMBER_ME                = "RememberMe";
		public const string             USER_COOCKIE_NAME               = "ECOM_User_Info";
		public const string             CURRENT_ACCOUNT                 = "CurrentAccount";
		public const string             CURRENT_ORDER_ID                = "CurrentOrderID";
		public const string             PENDING_ORDER_ITEMS             = "PendingOrderItems";

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
		public const string             ACTION_ADD                      = "Add";
		public const string             ACTION_EDIT                     = "Edit";
		public const string             ACTION_LIST                     = "List";
		public const string             ACTION_DELETE                   = "Delete";
		public const string             ACTION_VIEW						= "View";
		public const string             ACTION_PRODUCT_VIEW             = "ProductView";
		public const string             ACTION_REMOVE_ITEM				= "RemoveItem";
		public const string             ACTION_REMOVE_ORDER             = "RemoveOrder";
		public const string             ACTION_EXPORT_TO_EXCEL          = "ExportToExcel";

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
