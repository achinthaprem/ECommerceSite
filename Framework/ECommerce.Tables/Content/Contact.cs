/*----------------------------------------------------------------------------------------
* Page   : Contact
* File   : Contact.cs
* Author : Volume RADA Generator (Version 6.1)
* Date   : 2017-09-14
* Comment: Created by Author
* --------------------------------------------------------------------------------------*/

/*---------------------------------Version Control---------------------------------------
 *
 * DATE						 WHO                ACTION
 * 2017-09-14  Volume RADA Generator (Version 6.1)	Created
 *
 *-------------------------------End Version Control-------------------------------------*/

using System;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Collections.Generic;

namespace ECommerce.Tables.Content
{
	/// <summary>
	/// This is the class to define the table Contact
	/// </summary>

	public class Contact : Object, IComparable
	{

		#region Read Status Constants

		/// <summary>
		/// Message has read.
		/// </summary>
		public const int        STATUS_READ             = 1;

		/// <summary>
		/// Message has not read.
		/// </summary>
		public const int        STATUS_UNREAD           = 0;

		#endregion

		#region Column Fields

		// V2Generator: Section Start : Database Column Field Names
		/// <summary>
		/// No information available for ID
		/// </summary>
		protected const string Field_ID                                                                      = "ID";
		/// <summary>
		/// No information available for Name
		/// </summary>
		protected const string Field_name                                                                    = "name";
		/// <summary>
		/// No information available for Email
		/// </summary>
		protected const string Field_email                                                                   = "email";
		/// <summary>
		/// No information available for ContactNo
		/// </summary>
		protected const string Field_contact_no                                                              = "contact_no";
		/// <summary>
		/// No information available for Subject
		/// </summary>
		protected const string Field_subject                                                                 = "subject";
		/// <summary>
		/// No information available for Message
		/// </summary>
		protected const string Field_message                                                                 = "message";
		/// <summary>
		/// No information available for DateCreated
		/// </summary>
		protected const string Field_date                                                                    = "date";
		/// <summary>
		/// No information available for ReadStatus
		/// </summary>
		protected const string Field_read_status                                                             = "read_status";
		// V2Generator: Section End :Database Column Field Names
		#endregion

		#region Protected Fields

		// V2Generator: Section Start : Protected fields section
		/// <summary>
		/// No information available for ID
		/// </summary>
		protected int       m_ID                                         = -1;
		/// <summary>
		/// No information available for Name
		/// </summary>
		protected string    m_Name                                       = null;
		/// <summary>
		/// No information available for Email
		/// </summary>
		protected string    m_Email                                      = null;
		/// <summary>
		/// No information available for ContactNo
		/// </summary>
		protected string    m_ContactNo                                  = null;
		/// <summary>
		/// No information available for Subject
		/// </summary>
		protected string    m_Subject                                    = null;
		/// <summary>
		/// No information available for Message
		/// </summary>
		protected string    m_Message                                    = null;
		/// <summary>
		/// No information available for DateCreated
		/// </summary>
		protected DateTime  m_Date                                       = Convert.ToDateTime("1753/01/01 12:00:00");
		/// <summary>
		/// No information available for ReadStatus
		/// </summary>
		protected int       m_ReadStatus                                 = -1;
		// V2Generator: Section End :Protected fields section
		#endregion

		#region List Item

		// V2Generator: Section Start : List Item Attributes
		// V2Generator: Section End :List Item Attributes
		#endregion

		#region Constructors

		/// <summary>
		/// Creates a new Contact object
		/// </summary>
		/// <param name="ID">No information available for ID</param>
		/// <param name="Name">No information available for Name</param>
		/// <param name="Email">No information available for Email</param>
		/// <param name="ContactNo">No information available for ContactNo</param>
		/// <param name="Subject">No information available for Subject</param>
		/// <param name="Message">No information available for Message</param>
		/// <param name="Date">No information available for DateCreated</param>
		/// <param name="ReadStatus">No information available for ReadStatus</param>
		/// <returns></returns>
		// V2Generator: Section Start : Constructor using all the fields
		protected Contact(
			int ID,
			string Name,
			string Email,
			string ContactNo,
			string Subject,
			string Message,
			DateTime Date,
			int ReadStatus)
		{
			// V2Generator: Body Start
			this.m_ID                                    = ID;
			this.m_Name                                  = Name;
			this.m_Email                                 = Email;
			this.m_ContactNo                             = ContactNo;
			this.m_Subject                               = Subject;
			this.m_Message                               = Message;
			this.m_Date                                  = Date;
			this.m_ReadStatus                            = ReadStatus;
			// V2Generator: Body End
		}
		// V2Generator: Section End :Constructor using all the fields

		/// <summary>
		/// Creates a new Contact object
		/// </summary>
		/// <param name="Name">No information available for Name</param>
		/// <param name="Email">No information available for Email</param>
		/// <param name="ContactNo">No information available for ContactNo</param>
		/// <param name="Subject">No information available for Subject</param>
		/// <param name="Message">No information available for Message</param>
		/// <param name="ReadStatus">No information available for ReadStatus</param>
		/// <returns></returns>
		// V2Generator: Section Start : Constructor for insert
		protected Contact(
			string Name,
			string Email,
			string ContactNo,
			string Subject,
			string Message,
			int ReadStatus)
		{
			// V2Generator: Body Start
			this.m_Name                             = Name;
			this.m_Email                            = Email;
			this.m_ContactNo                        = ContactNo;
			this.m_Subject                          = Subject;
			this.m_Message                          = Message;
			this.m_ReadStatus                       = ReadStatus;
			// V2Generator: Body End
		}
		// V2Generator: Section End :Constructor for insert

		#endregion

		#region Execute Create

		/// <summary>
		/// Creates a new Contact object based on the given record id from the database
		/// </summary>
		/// <param name="ID">No information available for ID</param>
		/// <returns>null or a new Contact object</returns>
		// V2Generator: Section Start : ExecuteCreate By ID
		public static Contact ExecuteCreate(int ID)
		{
			// V2Generator: Body Start
			Contact                       result     = null;

			DataTable                     dt          = ECommerce.SQL.Content.Contact.ContactGetByID(ID);

			if (dt.Rows.Count>0)
			{
				result                                  = Create(dt.Rows[0]);
			}

			return result;
			// V2Generator: Body End
		}
		// V2Generator: Section End :ExecuteCreate By ID

		/// <summary>
		/// Creates a new Contact object for inserting a new record into the database
		/// </summary>
		/// <param name="Name">No information available for Name</param>
		/// <param name="Email">No information available for Email</param>
		/// <param name="ContactNo">No information available for ContactNo</param>
		/// <param name="Subject">No information available for Subject</param>
		/// <param name="Message">No information available for Message</param>
		/// <param name="ReadStatus">No information available for ReadStatus</param>
		/// <returns>null or a new Contact object</returns>
		/// <Exception cref="System.Exception">
		/// Throws an exception if Name is null. Throws an exception if Email is null. Throws an exception if ContactNo is null. Throws an exception if Subject is null. Throws an exception if Message is null. 
		/// </Exception>
		// V2Generator: Section Start : ExecuteCreate Insert
		public static Contact ExecuteCreate(
			string Name,
			string Email,
			string ContactNo,
			string Subject,
			string Message,
			int ReadStatus)
		{
			// V2Generator: Body Start
			Contact result      = null;

			#region Validate the input parameter(s)

			if (Name == null)
			{
				throw new System.Exception("Contact ExecuteCreate :: Name cannot be null (Database Constraint)");
			}
			if (Email == null)
			{
				throw new System.Exception("Contact ExecuteCreate :: Email cannot be null (Database Constraint)");
			}
			if (ContactNo == null)
			{
				throw new System.Exception("Contact ExecuteCreate :: ContactNo cannot be null (Database Constraint)");
			}
			if (Subject == null)
			{
				throw new System.Exception("Contact ExecuteCreate :: Subject cannot be null (Database Constraint)");
			}
			if (Message == null)
			{
				throw new System.Exception("Contact ExecuteCreate :: Message cannot be null (Database Constraint)");
			}
			#endregion

			result          = new Contact(Name, Email, ContactNo, Subject, Message, ReadStatus);
			return result;
			// V2Generator: Body End
		}
		// V2Generator: Section End :ExecuteCreate Insert

		/// <summary>
		/// Creates a new Contact object
		/// </summary>
		/// <param name="row"></param>
		/// <returns>null or a new Contact object</returns>
		// V2Generator: Section Start : ExecuteCreate Row
		internal static Contact ExecuteCreate(DataRow row)
		{
			// V2Generator: Body Start
			return Create(row);
			// V2Generator: Body End
		}
		// V2Generator: Section End :ExecuteCreate Row

		#endregion

		#region Create

		/// <summary>
		/// Creates a new Contact object based on the given row obtained from the database. The row is from the table Contact.
		/// </summary>
		/// <param name="row"></param>
		/// <returns>null or a Contact object</returns>
		// V2Generator: Section Start : Create section
		private static Contact Create(DataRow row)
		{
			// V2Generator: Body Start
			Contact    result            = null;

			int        ID                = -1;
			string     Name              = null;
			string     Email             = null;
			string     ContactNo         = null;
			string     Subject           = null;
			string     Message           = null;
			DateTime   Date              = Convert.ToDateTime("1753/01/01 12:00:00");
			int        ReadStatus        = -1;

			#region get the values
			ID                               = (int)row[Field_ID];
			if (row[Field_name]!=System.DBNull.Value)
			{
				Name                         = (string)row[Field_name];
			}
			if (row[Field_email]!=System.DBNull.Value)
			{
				Email                        = (string)row[Field_email];
			}
			if (row[Field_contact_no]!=System.DBNull.Value)
			{
				ContactNo                    = (string)row[Field_contact_no];
			}
			if (row[Field_subject]!=System.DBNull.Value)
			{
				Subject                      = (string)row[Field_subject];
			}
			if (row[Field_message]!=System.DBNull.Value)
			{
				Message                      = (string)row[Field_message];
			}
			if (row[Field_date]!=System.DBNull.Value)
			{
				Date                         = (DateTime)row[Field_date];
			}
			if (row[Field_read_status]!=System.DBNull.Value)
			{
				ReadStatus                   = (int)row[Field_read_status];
			}
			#endregion
			result                           = new Contact(ID, Name, Email, ContactNo, Subject, Message, Date, ReadStatus);

			return result;
			// V2Generator: Body End
		}
		// V2Generator: Section End :Create section

		#endregion

		#region Relation

		/// <summary>
		/// Creates relationship between Contact and primary tables
		/// </summary>
		/// <returns>null or a ContactDataTable</returns>
		// V2Generator: Section Start : Relations
		protected static DataTable AddRelation()
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
		public static List<Contact> List()
		{
			// V2Generator: Body Start
			List<Contact>           list                = new List<Contact>();
			DataTable           dt                  = ECommerce.SQL.Content.Contact.ContactList();

			for (int i = 0; i<dt.Rows.Count; i++)
			{
				Contact         result              = Create(dt.Rows[i]);
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

		/// <summary>
		/// Lists all the records from the database
		/// </summary>
		/// <param name="ReadStatus">The condition element in the database</param>
		/// <returns></returns>
		// V2Generator: Section Start : ListAllByReadStatus
		public static List<Contact> ListByReadStatus(int ReadStatus)
		{
			// V2Generator: Body Start
			List<Contact>           list                = new List<Contact>();
			DataTable           dt                  = ECommerce.SQL.Content.Contact.ContactListByReadStatus(ReadStatus);

			for (int i = 0; i<dt.Rows.Count; i++)
			{
				Contact         result              = Create(dt.Rows[i]);
				if (result!=null)
				{
					list.Add(result);
				}
			}

			list.Sort();

			return list;
			// V2Generator: Body End
		}
		// V2Generator: Section End :ListAllByReadStatus


		#endregion

		#region DB Methods

		/// <summary>
		/// Deletes the object record from the database (Does not act as a destructor)
		/// </summary>
		/// <returns></returns>
		// V2Generator: Section Start : Delete
		public void Delete()
		{
			// V2Generator: Body Start
			ECommerce.SQL.Content.Contact.ContactDelete(this.m_ID);
			// V2Generator: Body End
		}
		// V2Generator: Section End :Delete

		/// <summary>
		/// Updates the object members and the corresponding row in the database.
		/// </summary>
		/// <param name="Name">No information available for Name</param>
		/// <param name="Email">No information available for Email</param>
		/// <param name="ContactNo">No information available for ContactNo</param>
		/// <param name="Subject">No information available for Subject</param>
		/// <param name="Message">No information available for Message</param>
		/// <param name="Date">No information available for DateCreated</param>
		/// <param name="ReadStatus">No information available for ReadStatus</param>
		/// <returns></returns>
		// V2Generator: Section Start : Update
		protected void Update(
			string Name,
			string Email,
			string ContactNo,
			string Subject,
			string Message,
			DateTime Date,
			int ReadStatus)
		{
			// V2Generator: Body Start
			ECommerce.SQL.Content.Contact.ContactUpdate(ID, Name, Email, ContactNo, Subject, Message, Date, ReadStatus);

			this.m_Name                   = Name;
			this.m_Email                  = Email;
			this.m_ContactNo              = ContactNo;
			this.m_Subject                = Subject;
			this.m_Message                = Message;
			this.m_Date                   = Date;
			this.m_ReadStatus             = ReadStatus;
			// V2Generator: Body End
		}
		// V2Generator: Section End :Update

		public void UpdateReadStatus(int ReadStatus)
		{
			this.Update(this.m_Name, this.m_Email, this.m_ContactNo, this.m_Subject, this.m_Message, this.m_Date, ReadStatus);
		}

		/// <summary>
		/// Inserts a new record into the database, and updates the identity field in this object.
		/// </summary>
		/// <returns></returns>
		// V2Generator: Section Start : Insert
		public void Insert()
		{
			// V2Generator: Body Start
			this.m_Date         = DateTime.Now;
			this.m_ID           = ECommerce.SQL.Content.Contact.ContactInsert(this.m_Name, this.m_Email, this.m_ContactNo, this.m_Subject, this.m_Message, this.m_Date, this.m_ReadStatus);
			// V2Generator: Body End
		}
		// V2Generator: Section End :Insert

		#endregion

		#region Compare

		/// <summary>
		/// Compares the current Contact object with the given Contact object
		/// </summary>
		/// <param name="row">The Contact object to compare with</param>
		/// <returns>The compared difference between the Contact objects</returns>
		// V2Generator: Section Start : CompareTo
		public int CompareTo(object row)
		{
			// V2Generator: Body Start
			if (row is Contact)
			{
				Contact temp = (Contact) row;
				return ID.CompareTo(temp.ID);
			}
			else
			{
				throw new System.ArgumentException("object is not a Contact");
			}
			// V2Generator: Body End
		}
		// V2Generator: Section End :CompareTo

		/// <summary>
		/// Get HashCode
		/// </summary>
		/// <returns>HashCode</returns>
		// V2Generator: Section Start : GetHashCode
		public override int GetHashCode()
		{
			// V2Generator: Body Start
			return base.GetHashCode();
			// V2Generator: Body End
		}
		// V2Generator: Section End :GetHashCode

		/// <summary>
		/// Equals from Object class
		/// </summary>
		/// <param name="obj">The Contact object to compare with</param>
		/// <returns>True if equal else false</returns>
		// V2Generator: Section Start : Equals
		public override bool Equals(object obj)
		{
			// V2Generator: Body Start
			bool  result     = false;
			if (this.ID ==((Contact)obj).ID)
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
			get { return this.m_ID; }
		}
		/// <summary>
		/// No information available for Name
		/// </summary>
		public string Name
		{
			get { return this.m_Name; }
		}
		/// <summary>
		/// No information available for Email
		/// </summary>
		public string Email
		{
			get { return this.m_Email; }
		}
		/// <summary>
		/// No information available for ContactNo
		/// </summary>
		public string ContactNo
		{
			get { return this.m_ContactNo; }
		}
		/// <summary>
		/// No information available for Subject
		/// </summary>
		public string Subject
		{
			get { return this.m_Subject; }
		}
		/// <summary>
		/// No information available for Message
		/// </summary>
		public string Message
		{
			get { return this.m_Message; }
		}
		/// <summary>
		/// No information available for DateCreated
		/// </summary>
		public DateTime Date
		{
			get { return this.m_Date; }
		}
		/// <summary>
		/// No information available for ReadStatus
		/// </summary>
		public int ReadStatus
		{
			get { return this.m_ReadStatus; }
		}
		// V2Generator: Section End :Assessors section
		// V2Generator: Section Start : List Item Assessor
		// V2Generator: Section End :List Item Assessor
		#endregion

	}

}

