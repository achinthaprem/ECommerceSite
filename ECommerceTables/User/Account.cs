/*----------------------------------------------------------------------------------------
* Page   : Account
* File   : Account.cs
* Author : Volume RADA Generator (Version 6.1)
* Date   : 2017-08-23
* Comment: Created by Author
* --------------------------------------------------------------------------------------*/

/*---------------------------------Version Control---------------------------------------
 *
 * DATE						 WHO                ACTION
 * 2017-08-23  Volume RADA Generator (Version 6.1)	Created
 *
 *-------------------------------End Version Control-------------------------------------*/

using System;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Collections.Generic;
using System.Security.Principal;

namespace ECommerceTables
{
	/// <summary>
	/// This is the class to define the table Account
	/// </summary>

	public class Account : GenericPrincipal, IComparable
	{

		#region Column Fields

		// V2Generator: Section Start : Database Column Field Names
		/// <summary>
		/// No information available for ID
		/// </summary>
		protected const string Field_ID																		 = "ID";
		/// <summary>
		/// No information available for firstName
		/// </summary>
		protected const string Field_first_name																 = "first_name";
		/// <summary>
		/// No information available for lastName
		/// </summary>
		protected const string Field_last_name 																 = "last_name";
		/// <summary>
		/// No information available for email
		/// </summary>
		protected const string Field_email 																	 = "email";
		/// <summary>
		/// No information available for password
		/// </summary>
		protected const string Field_password  																 = "password";
		/// <summary>
		/// No information available for salt
		/// </summary>
		protected const string Field_salt  																	 = "salt";
		/// <summary>
		/// No information available for contactNo
		/// </summary>
		protected const string Field_contact_no																 = "contact_no";
		/// <summary>
		/// No information available for shippingAddress
		/// </summary>
		protected const string Field_shipping_address  														 = "shipping_address";
		/// <summary>
		/// No information available for country
		/// </summary>
		protected const string Field_country   																 = "country";
		/// <summary>
		/// No information available for status
		/// </summary>
		protected const string Field_status																	 = "status";
		/// <summary>
		/// No information available for role
		/// </summary>
		protected const string Field_role  																	 = "role";
		/// <summary>
		/// No information available for dateCreated
		/// </summary>
		protected const string Field_date_created  															 = "date_created";
		/// <summary>
		/// No information available for dateModified
		/// </summary>
		protected const string Field_date_modified 															 = "date_modified";
		/// <summary>
		/// No information available for createdAccountID
		/// </summary>
		protected const string Field_created_account_id														 = "created_account_id";
		/// <summary>
		/// No information available for modifiedAccountID
		/// </summary>
		protected const string Field_modified_account_id   													 = "modified_account_id";
		// V2Generator: Section End :Database Column Field Names
		#endregion

		#region Protected Fields

		// V2Generator: Section Start : Protected fields section
		/// <summary>
		/// No information available for ID
		/// </summary>
		protected int 		m_ID										 = -1;
		/// <summary>
		/// No information available for firstName
		/// </summary>
		protected string  	m_FirstName 								 = null;
		/// <summary>
		/// No information available for lastName
		/// </summary>
		protected string  	m_LastName  								 = null;
		/// <summary>
		/// No information available for email
		/// </summary>
		protected string  	m_Email 									 = null;
		/// <summary>
		/// No information available for password
		/// </summary>
		protected string  	m_Password  								 = null;
		/// <summary>
		/// No information available for salt
		/// </summary>
		protected string  	m_Salt  									 = null;
		/// <summary>
		/// No information available for contactNo
		/// </summary>
		protected string  	m_ContactNo 								 = null;
		/// <summary>
		/// No information available for shippingAddress
		/// </summary>
		protected string  	m_ShippingAddress   						 = null;
		/// <summary>
		/// No information available for country
		/// </summary>
		protected string  	m_Country   								 = null;
		/// <summary>
		/// No information available for status
		/// </summary>
		protected int 		m_Status									 = -1;
		/// <summary>
		/// No information available for role
		/// </summary>
		protected int 		m_Role  									 = -1;
		/// <summary>
		/// No information available for dateCreated
		/// </summary>
		protected DateTime	m_DateCreated   							 = Convert.ToDateTime("1753/01/01 12:00:00");
		/// <summary>
		/// No information available for dateModified
		/// </summary>
		protected DateTime	m_DateModified  							 = Convert.ToDateTime("1753/01/01 12:00:00");
		/// <summary>
		/// No information available for createdAccountID
		/// </summary>
		protected int 		m_CreatedAccountID  						 = -1;
		/// <summary>
		/// No information available for modifiedAccountID
		/// </summary>
		protected int 		m_ModifiedAccountID 						 = -1;
		// V2Generator: Section End :Protected fields section
		#endregion

		#region List Item

		// V2Generator: Section Start : List Item Attributes
		// V2Generator: Section End :List Item Attributes
		#endregion

		#region Constructors

		/// <summary>
		/// Creates a new Account object
		/// </summary>
		/// <param name="ID">No information available for ID</param>
		/// <param name="FirstName">No information available for firstName</param>
		/// <param name="LastName">No information available for lastName</param>
		/// <param name="Email">No information available for email</param>
		/// <param name="Password">No information available for password</param>
		/// <param name="Salt">No information available for salt</param>
		/// <param name="ContactNo">No information available for contactNo</param>
		/// <param name="ShippingAddress">No information available for shippingAddress</param>
		/// <param name="Country">No information available for country</param>
		/// <param name="Status">No information available for status</param>
		/// <param name="Role">No information available for role</param>
		/// <param name="DateCreated">No information available for dateCreated</param>
		/// <param name="DateModified">No information available for dateModified</param>
		/// <param name="CreatedAccountID">No information available for createdAccountID</param>
		/// <param name="ModifiedAccountID">No information available for modifiedAccountID</param>
		/// <returns></returns>
		// V2Generator: Section Start : Constructor using all the fields
		protected Account (
			int ID,
			string FirstName,
			string LastName,
			string Email,
			string Password,
			string Salt,
			string ContactNo,
			string ShippingAddress,
			string Country,
			int Status,
			int Role,
			DateTime DateCreated,
			DateTime DateModified,
			int CreatedAccountID,
			int ModifiedAccountID) : base(new GenericIdentity(Email), null)
		{
			// V2Generator: Body Start
			this.m_ID   								 = ID;
			this.m_FirstName							 = FirstName;
			this.m_LastName 							 = LastName;
			this.m_Email								 = Email;
			this.m_Password 							 = Password;
			this.m_Salt 								 = Salt;
			this.m_ContactNo							 = ContactNo;
			this.m_ShippingAddress  					 = ShippingAddress;
			this.m_Country  							 = Country;
			this.m_Status   							 = Status;
			this.m_Role 								 = Role;
			this.m_DateCreated  						 = DateCreated;
			this.m_DateModified 						 = DateModified;
			this.m_CreatedAccountID 					 = CreatedAccountID;
			this.m_ModifiedAccountID					 = ModifiedAccountID;
			// V2Generator: Body End
		}
		// V2Generator: Section End :Constructor using all the fields

		/// <summary>
		/// Creates a new Account object
		/// </summary>
		/// <param name="FirstName">No information available for firstName</param>
		/// <param name="LastName">No information available for lastName</param>
		/// <param name="Email">No information available for email</param>
		/// <param name="Password">No information available for password</param>
		/// <param name="Salt">No information available for salt</param>
		/// <param name="ContactNo">No information available for contactNo</param>
		/// <param name="ShippingAddress">No information available for shippingAddress</param>
		/// <param name="Country">No information available for country</param>
		/// <param name="Status">No information available for status</param>
		/// <param name="Role">No information available for role</param>
		/// <param name="DateCreated">No information available for dateCreated</param>
		/// <param name="DateModified">No information available for dateModified</param>
		/// <param name="CreatedAccountID">No information available for createdAccountID</param>
		/// <param name="ModifiedAccountID">No information available for modifiedAccountID</param>
		/// <returns></returns>
		// V2Generator: Section Start : Constructor for insert
		protected Account (
			string FirstName,
			string LastName,
			string Email,
			string Password,
			string Salt,
			string ContactNo,
			string ShippingAddress,
			string Country,
			int Status,
			int Role,
			DateTime DateCreated,
			DateTime DateModified,
			int CreatedAccountID,
			int ModifiedAccountID) : base(new GenericIdentity(Email), null)
		{
			// V2Generator: Body Start
			this.m_FirstName						= FirstName;
			this.m_LastName 						= LastName;
			this.m_Email							= Email;
			this.m_Password 						= Password;
			this.m_Salt 							= Salt;
			this.m_ContactNo						= ContactNo;
			this.m_ShippingAddress  				= ShippingAddress;
			this.m_Country  						= Country;
			this.m_Status   						= Status;
			this.m_Role 							= Role;
			this.m_DateCreated  					= DateCreated;
			this.m_DateModified 					= DateModified;
			this.m_CreatedAccountID 				= CreatedAccountID;
			this.m_ModifiedAccountID				= ModifiedAccountID;
			// V2Generator: Body End
		}
		// V2Generator: Section End :Constructor for insert

		#endregion

		#region Execute Create

		/// <summary>
		/// Creates a new Account object based on the given record id from the database
		/// </summary>
		/// <param name="ID">No information available for ID</param>
		/// <returns>null or a new Account object</returns>
		// V2Generator: Section Start : ExecuteCreate By ID
		public static Account ExecuteCreate (int ID)
		{
			// V2Generator: Body Start
			Account 					  result     = null;

			DataTable   				  dt  		  = ECommerceSql.Account.AccountGetByID(ID);

			if (dt.Rows.Count>0)
			{
				result									= Create(dt.Rows[0]);
			}

			return result;
			// V2Generator: Body End
		}
		// V2Generator: Section End :ExecuteCreate By ID

		/// <summary>
		/// Creates a new Account object for inserting a new record into the database
		/// </summary>
		/// <param name="FirstName">No information available for firstName</param>
		/// <param name="LastName">No information available for lastName</param>
		/// <param name="Email">No information available for email</param>
		/// <param name="Password">No information available for password</param>
		/// <param name="Salt">No information available for salt</param>
		/// <param name="ContactNo">No information available for contactNo</param>
		/// <param name="ShippingAddress">No information available for shippingAddress</param>
		/// <param name="Country">No information available for country</param>
		/// <param name="Status">No information available for status</param>
		/// <param name="Role">No information available for role</param>
		/// <param name="DateCreated">No information available for dateCreated</param>
		/// <param name="DateModified">No information available for dateModified</param>
		/// <param name="CreatedAccountID">No information available for createdAccountID</param>
		/// <param name="ModifiedAccountID">No information available for modifiedAccountID</param>
		/// <returns>null or a new Account object</returns>
		/// <Exception cref="System.Exception">
		/// Throws an exception if FirstName is null. Throws an exception if LastName is null. Throws an exception if Email is null. Throws an exception if Password is null. Throws an exception if Salt is null. Throws an exception if ContactNo is null. Throws an exception if ShippingAddress is null. Throws an exception if Country is null. Throws an exception if DateCreated is DateTime.MinValue. Throws an exception if DateModified is DateTime.MinValue. 
		/// </Exception>
		// V2Generator: Section Start : ExecuteCreate Insert
		public static Account ExecuteCreate (
			string FirstName,
			string LastName,
			string Email,
			string Password,
			string Salt,
			string ContactNo,
			string ShippingAddress,
			string Country,
			int Status,
			int Role,
			DateTime DateCreated,
			DateTime DateModified,
			int CreatedAccountID,
			int ModifiedAccountID)
		{
			// V2Generator: Body Start
			Account	result		= null;

			#region Validate the input parameter(s)

			if (FirstName == null)
			{
				throw new System.Exception("Account ExecuteCreate :: FirstName cannot be null (Database Constraint)");
			}
			if (LastName == null)
			{
				throw new System.Exception("Account ExecuteCreate :: LastName cannot be null (Database Constraint)");
			}
			if (Email == null)
			{
				throw new System.Exception("Account ExecuteCreate :: Email cannot be null (Database Constraint)");
			}
			if (Password == null)
			{
				throw new System.Exception("Account ExecuteCreate :: Password cannot be null (Database Constraint)");
			}
			if (Salt == null)
			{
				throw new System.Exception("Account ExecuteCreate :: Salt cannot be null (Database Constraint)");
			}
			if (ContactNo == null)
			{
				throw new System.Exception("Account ExecuteCreate :: ContactNo cannot be null (Database Constraint)");
			}
			if (ShippingAddress == null)
			{
				throw new System.Exception("Account ExecuteCreate :: ShippingAddress cannot be null (Database Constraint)");
			}
			if (Country == null)
			{
				throw new System.Exception("Account ExecuteCreate :: Country cannot be null (Database Constraint)");
			}
			if (DateCreated == DateTime.MinValue)
			{
				throw new System.Exception("Account ExecuteCreate :: DateCreated cannot be DateTime.MinValue");
			}
			if (DateModified == DateTime.MinValue)
			{
				throw new System.Exception("Account ExecuteCreate :: DateModified cannot be DateTime.MinValue");
			}
			#endregion

			result			= new Account(FirstName, LastName, Email, Password, Salt, ContactNo, ShippingAddress, Country, Status, Role, DateCreated, DateModified, CreatedAccountID, ModifiedAccountID);
			return result;
			// V2Generator: Body End
		}
		// V2Generator: Section End :ExecuteCreate Insert

		/// <summary>
		/// Creates a new Account object
		/// </summary>
		/// <param name="row"></param>
		/// <returns>null or a new Account object</returns>
		// V2Generator: Section Start : ExecuteCreate Row
		internal static Account ExecuteCreate (DataRow row)
		{
			// V2Generator: Body Start
			return Create(row);
			// V2Generator: Body End
		}
		// V2Generator: Section End :ExecuteCreate Row

		#endregion

		#region Create

		/// <summary>
		/// Creates a new Account object based on the given row obtained from the database. The row is from the table Account.
		/// </summary>
		/// <param name="row"></param>
		/// <returns>null or a Account object</returns>
		// V2Generator: Section Start : Create section
		private static Account Create (DataRow row)
		{
			// V2Generator: Body Start
			Account    result  			 = null;
			
			int 	   ID  				 = -1;
			string     FirstName   		 = null;
			string     LastName			 = null;
			string     Email   			 = null;
			string     Password			 = null;
			string     Salt				 = null;
			string     ContactNo   		 = null;
			string     ShippingAddress 	 = null;
			string     Country 			 = null;
			int 	   Status  			 = -1;
			int 	   Role				 = -1;
			DateTime   DateCreated 		 = Convert.ToDateTime("1753/01/01 12:00:00");
			DateTime   DateModified		 = Convert.ToDateTime("1753/01/01 12:00:00");
			int 	   CreatedAccountID	 = -1;
			int 	   ModifiedAccountID    = -1;

			#region get the values
			ID  							 = (int) row[Field_ID];
			if (row[Field_first_name]!=System.DBNull.Value)
			{
				FirstName   				 = (string) row[Field_first_name];
			}
			if (row[Field_last_name]!=System.DBNull.Value)
			{
				LastName					 = (string) row[Field_last_name];
			}
			if (row[Field_email]!=System.DBNull.Value)
			{
				Email   					 = (string) row[Field_email];
			}
			if (row[Field_password]!=System.DBNull.Value)
			{
				Password					 = (string) row[Field_password];
			}
			if (row[Field_salt]!=System.DBNull.Value)
			{
				Salt						 = (string) row[Field_salt];
			}
			if (row[Field_contact_no]!=System.DBNull.Value)
			{
				ContactNo   				 = (string) row[Field_contact_no];
			}
			if (row[Field_shipping_address]!=System.DBNull.Value)
			{
				ShippingAddress 			 = (string) row[Field_shipping_address];
			}
			if (row[Field_country]!=System.DBNull.Value)
			{
				Country 					 = (string) row[Field_country];
			}
			if (row[Field_status]!=System.DBNull.Value)
			{
				Status  					 = (int) row[Field_status];
			}
			if (row[Field_role]!=System.DBNull.Value)
			{
				Role						 = (int) row[Field_role];
			}
			if (row[Field_date_created]!=System.DBNull.Value)
			{
				DateCreated 				 = (DateTime) row[Field_date_created];
			}
			if (row[Field_date_modified]!=System.DBNull.Value)
			{
				DateModified				 = (DateTime) row[Field_date_modified];
			}
			if (row[Field_created_account_id]!=System.DBNull.Value)
			{
				CreatedAccountID			 = (int) row[Field_created_account_id];
			}
			if (row[Field_modified_account_id]!=System.DBNull.Value)
			{
				ModifiedAccountID   		 = (int) row[Field_modified_account_id];
			}
			#endregion
			result  						 = new Account(ID, FirstName, LastName, Email, Password, Salt, ContactNo, ShippingAddress, Country, Status, Role, DateCreated, DateModified, CreatedAccountID, ModifiedAccountID);

			return result;
			// V2Generator: Body End
		}
		// V2Generator: Section End :Create section

		#endregion

		#region Relation

		/// <summary>
		/// Creates relationship between Account and primary tables
		/// </summary>
		/// <returns>null or a AccountDataTable</returns>
		// V2Generator: Section Start : Relations
		protected static DataTable  AddRelation ()
		{
			// V2Generator: Body Start
		return null;
			// V2Generator: Body End
		}
		// V2Generator: Section End :Relations

		#endregion

		#region List Methods

		/// <summary>
		/// Lists all the records from the database
		/// </summary>
		/// <returns></returns>
		// V2Generator: Section Start : List all
		public static List<Account> List ()
		{
			// V2Generator: Body Start
			List<Account>			list				= new List<Account>();
			DataTable			dt					= ECommerceSql.Account.AccountList();

			for (int i=0; i<dt.Rows.Count; i++)
			{
				Account 		result				= Create(dt.Rows[i]);
				if (result!=null)
				{
					list.Add(result);
				}
			}

			list.Sort();

			return list;
			// V2Generator: Body End
		}
		// V2Generator: Section End :List all


		#endregion

		#region DB Methods

		/// <summary>
		/// Deletes the object record from the database (Does not act as a destructor)
		/// </summary>
		/// <returns></returns>
		// V2Generator: Section Start : Delete
		public void Delete ()
		{
			// V2Generator: Body Start
			ECommerceSql.Account.AccountDelete (this.m_ID);
			// V2Generator: Body End
		}
		// V2Generator: Section End :Delete

		/// <summary>
		/// Updates the object members and the corresponding row in the database.
		/// </summary>
		/// <param name="FirstName">No information available for firstName</param>
		/// <param name="LastName">No information available for lastName</param>
		/// <param name="Email">No information available for email</param>
		/// <param name="Password">No information available for password</param>
		/// <param name="Salt">No information available for salt</param>
		/// <param name="ContactNo">No information available for contactNo</param>
		/// <param name="ShippingAddress">No information available for shippingAddress</param>
		/// <param name="Country">No information available for country</param>
		/// <param name="Status">No information available for status</param>
		/// <param name="Role">No information available for role</param>
		/// <param name="DateCreated">No information available for dateCreated</param>
		/// <param name="DateModified">No information available for dateModified</param>
		/// <param name="CreatedAccountID">No information available for createdAccountID</param>
		/// <param name="ModifiedAccountID">No information available for modifiedAccountID</param>
		/// <returns></returns>
		// V2Generator: Section Start : Update
		protected void Update (
			string FirstName,
			string LastName,
			string Email,
			string Password,
			string Salt,
			string ContactNo,
			string ShippingAddress,
			string Country,
			int Status,
			int Role,
			DateTime DateCreated,
			DateTime DateModified,
			int CreatedAccountID,
			int ModifiedAccountID)
		{
			// V2Generator: Body Start
			ECommerceSql.Account.AccountUpdate (ID, FirstName, LastName, Email, Password, Salt, ContactNo, ShippingAddress, Country, Status, Role, DateCreated, DateModified, CreatedAccountID, ModifiedAccountID);

			this.m_FirstName			  = FirstName;
			this.m_LastName 			  = LastName;
			this.m_Email				  = Email;
			this.m_Password 			  = Password;
			this.m_Salt 				  = Salt;
			this.m_ContactNo			  = ContactNo;
			this.m_ShippingAddress  	  = ShippingAddress;
			this.m_Country  			  = Country;
			this.m_Status   			  = Status;
			this.m_Role 				  = Role;
			this.m_DateCreated  		  = DateCreated;
			this.m_DateModified 		  = DateModified;
			this.m_CreatedAccountID 	  = CreatedAccountID;
			this.m_ModifiedAccountID	  = ModifiedAccountID;
			// V2Generator: Body End
		}
		// V2Generator: Section End :Update

		/// <summary>
		/// Inserts a new record into the database, and updates the identity field in this object.
		/// </summary>
		/// <returns></returns>
		// V2Generator: Section Start : Insert
		public void Insert ()
		{
			// V2Generator: Body Start
			this.m_ID			= ECommerceSql.Account.AccountInsert (this.m_FirstName, this.m_LastName, this.m_Email, this.m_Password, this.m_Salt, this.m_ContactNo, this.m_ShippingAddress, this.m_Country, this.m_Status, this.m_Role, this.m_DateCreated, this.m_DateModified, this.m_CreatedAccountID, this.m_ModifiedAccountID);
			// V2Generator: Body End
		}
		// V2Generator: Section End :Insert

		#endregion

		#region Compare

		/// <summary>
		/// Compares the current Account object with the given Account object
		/// </summary>
		/// <param name="row">The Account object to compare with</param>
		/// <returns>The compared difference between the Account objects</returns>
		// V2Generator: Section Start : CompareTo
		public int CompareTo (object row)
		{
			// V2Generator: Body Start
			if(row is Account)
			{
				Account temp = (Account) row;
				return ID.CompareTo(temp.ID);
			}
			else
			{
				throw new System.ArgumentException("object is not a Account");
			}
			// V2Generator: Body End
		}
		// V2Generator: Section End :CompareTo

		/// <summary>
		/// Get HashCode
		/// </summary>
		/// <returns>HashCode</returns>
		// V2Generator: Section Start : GetHashCode
		public override int  GetHashCode ()
		{
			// V2Generator: Body Start
		return base.GetHashCode ();
			// V2Generator: Body End
		}
		// V2Generator: Section End :GetHashCode

		/// <summary>
		/// Equals from Object class
		/// </summary>
		/// <param name="obj">The Account object to compare with</param>
		/// <returns>True if equal else false</returns>
		// V2Generator: Section Start : Equals
		public override bool  Equals (object obj)
		{
			// V2Generator: Body Start
		bool  result     = false;
		if (this.ID ==((Account)obj).ID)
		{
			result     = true;
		}
		 return result;
			// V2Generator: Body End
		}
		// V2Generator: Section End :Equals

		#endregion

		#region Assessors

		// V2Generator: Section Start : Assessors section
		/// <summary>
		/// No information available for ID
		/// </summary>
		public int ID
		{
			get{return this.m_ID;}
		}
		/// <summary>
		/// No information available for firstName
		/// </summary>
		public string FirstName
		{
			get{return this.m_FirstName;}
		}
		/// <summary>
		/// No information available for lastName
		/// </summary>
		public string LastName
		{
			get{return this.m_LastName;}
		}
		/// <summary>
		/// No information available for email
		/// </summary>
		public string Email
		{
			get{return this.m_Email;}
		}
		/// <summary>
		/// No information available for password
		/// </summary>
		public string Password
		{
			get{return this.m_Password;}
		}
		/// <summary>
		/// No information available for salt
		/// </summary>
		public string Salt
		{
			get{return this.m_Salt;}
		}
		/// <summary>
		/// No information available for contactNo
		/// </summary>
		public string ContactNo
		{
			get{return this.m_ContactNo;}
		}
		/// <summary>
		/// No information available for shippingAddress
		/// </summary>
		public string ShippingAddress
		{
			get{return this.m_ShippingAddress;}
		}
		/// <summary>
		/// No information available for country
		/// </summary>
		public string Country
		{
			get{return this.m_Country;}
		}
		/// <summary>
		/// No information available for status
		/// </summary>
		public int Status
		{
			get{return this.m_Status;}
		}
		/// <summary>
		/// No information available for role
		/// </summary>
		public int Role
		{
			get{return this.m_Role;}
		}
		/// <summary>
		/// No information available for dateCreated
		/// </summary>
		public DateTime DateCreated
		{
			get{return this.m_DateCreated;}
		}
		/// <summary>
		/// No information available for dateModified
		/// </summary>
		public DateTime DateModified
		{
			get{return this.m_DateModified;}
		}
		/// <summary>
		/// No information available for createdAccountID
		/// </summary>
		public int CreatedAccountID
		{
			get{return this.m_CreatedAccountID;}
		}
		/// <summary>
		/// No information available for modifiedAccountID
		/// </summary>
		public int ModifiedAccountID
		{
			get{return this.m_ModifiedAccountID;}
		}
		// V2Generator: Section End :Assessors section
		// V2Generator: Section Start : List Item Assessor
		// V2Generator: Section End :List Item Assessor
		#endregion

	}

}

