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
using System.Collections;
using System.Data;
using System.Data.SqlClient;


namespace ECommerce.SQL.Content
{
	/// <summary>
	/// This class is used to directly access (via Stored Procedures) the table Contact
	/// </summary>

	public class Contact
	{

		#region Basic Generated Stored Procedure Access Functions

		/// <summary>
		/// Accesses the stored procedure in the database ContactList to list all the records from Contact
		/// </summary>
		/// <returns>A DataTable object, or possibly null. The DataTable returned is a collection of all the rows from Contact</returns>
		// V2Generator: Section Start : List
		public static DataTable ContactList ()
		{
			// V2Generator: Body Start
			return SqlData.getSelectDataTable(SqlData.MASTER,"ContactList");
			// V2Generator: Body End
		}
		// V2Generator: Section End :List








		/// <summary>
		/// Accesses the stored procedure in the database ContactListByReadStatus to list all the records from Contact
		/// </summary>
		/// <param name="ReadStatus"></param>
		/// <returns>A DataTable object, or possibly null. The DataTable returned is a collection of all the rows from Contact</returns>
		// V2Generator: Section Start : ListByReadStatus
		public static DataTable ContactListByReadStatus (int ReadStatus)
		{
			// V2Generator: Body Start
			SqlParameter[] param			=
				{
					new SqlParameter("@read_status", SqlDbType.Int)
				};

			param[0].Value					= ReadStatus;

			return SqlData.getSelectDataTable(SqlData.MASTER,"ContactListByReadStatus", param);
			// V2Generator: Body End
		}
		// V2Generator: Section End :ListByReadStatus

		/// <summary>
		/// Accesses the stored procedure in the database ContactGetByID to obtain a record from Contact
		/// </summary>
		/// <param name="ID">No information available for ID</param>
		/// <returns>A DataTable object, or possibly null. The DataTable returned is a collection of all the rows from Contact</returns>
		// V2Generator: Section Start : Get By ID
		public static DataTable ContactGetByID (int ID)
		{
			// V2Generator: Body Start
			SqlParameter[] param			=
				{
					new SqlParameter("@ID", SqlDbType.Int)
				};

			param[0].Value					= ID;

			return SqlData.getSelectDataTable(SqlData.MASTER,"ContactGetByID", param);
			// V2Generator: Body End
		}
		// V2Generator: Section End :Get By ID

		/// <summary>
		/// Deletes a single record from Contact by selecting it by it's identifier
		/// </summary>
		/// <param name="ID">No information available for ID</param>
		/// <returns>Accesses the stored procedure in the database ContactDelete to delete a record from Contact</returns>
		// V2Generator: Section Start : Delete
		public static void ContactDelete (int ID)
		{
			// V2Generator: Body Start
			SqlParameter[] param			=
				{
					new SqlParameter("@ID", SqlDbType.Int)
				};

			param[0].Value					= ID;

			SqlData.getSelectDataTable(SqlData.MASTER,"ContactDelete", param);
			// V2Generator: Body End
		}
		// V2Generator: Section End :Delete

		/// <summary>
		/// Accesses the stored procedure in the database ContactInsert to insert a record into Contact
		/// </summary>
		/// <param name="Name">No information available for Name</param>
		/// <param name="Email">No information available for Email</param>
		/// <param name="ContactNo">No information available for ContactNo</param>
		/// <param name="Subject">No information available for Subject</param>
		/// <param name="Message">No information available for Message</param>
		/// <param name="Date">No information available for DateCreated</param>
		/// <param name="ReadStatus">No information available for ReadStatus</param>
		/// <returns>An integer id or -1</returns>
		// V2Generator: Section Start : Insert
		public static int ContactInsert (
			string Name,
			string Email,
			string ContactNo,
			string Subject,
			string Message,
			DateTime Date,
			int ReadStatus)
		{
			// V2Generator: Body Start
			SqlParameter[] param			=
				{
					new SqlParameter("@name", SqlDbType.NVarChar, 100) ,
					new SqlParameter("@email", SqlDbType.NVarChar, 255) ,
					new SqlParameter("@contact_no", SqlDbType.NVarChar, 30) ,
					new SqlParameter("@subject", SqlDbType.NVarChar, 50) ,
					new SqlParameter("@message", SqlDbType.NVarChar, 250) ,
					new SqlParameter("@date", SqlDbType.DateTime) ,
					new SqlParameter("@read_status", SqlDbType.Int)
				};

			param[0].Value					= Name;
			param[1].Value					= Email;
			param[2].Value					= ContactNo;
			param[3].Value					= Subject;
			param[4].Value					= Message;
			param[5].Value					= Date;
			param[6].Value					= ReadStatus;

			DataTable dt					= SqlData.getSelectDataTable(SqlData.MASTER,"ContactInsert",param);

			int rtn							= -1;
			if (dt != null)
			{
				if (dt.Rows.Count == 1)
				{
					rtn						= Convert.ToInt32(dt.Rows[0][0]);
				}
			}
			return rtn;
			// V2Generator: Body End
		}
		// V2Generator: Section End :Insert

		/// <summary>
		/// Accesses the stored procedure in the database ContactUpdate to update a record from Contact
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
		// V2Generator: Section Start : Update
		public static void ContactUpdate (
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
			SqlParameter[] param			=
				{
					new SqlParameter("@ID", SqlDbType.Int) ,
					new SqlParameter("@name", SqlDbType.NVarChar, 100) ,
					new SqlParameter("@email", SqlDbType.NVarChar, 255) ,
					new SqlParameter("@contact_no", SqlDbType.NVarChar, 30) ,
					new SqlParameter("@subject", SqlDbType.NVarChar, 50) ,
					new SqlParameter("@message", SqlDbType.NVarChar, 250) ,
					new SqlParameter("@date", SqlDbType.DateTime) ,
					new SqlParameter("@read_status", SqlDbType.Int)
				};

			param[0].Value					= ID;
			param[1].Value					= Name;
			param[2].Value					= Email;
			param[3].Value					= ContactNo;
			param[4].Value					= Subject;
			param[5].Value					= Message;
			param[6].Value					= Date;
			param[7].Value					= ReadStatus;

			SqlData.getSelectDataTable(SqlData.MASTER,"ContactUpdate", param);
			// V2Generator: Body End
		}
		// V2Generator: Section End :Update

		#endregion

	}
}
