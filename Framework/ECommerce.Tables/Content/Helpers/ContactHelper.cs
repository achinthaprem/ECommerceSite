using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Tables.Content.Helpers
{
	public class ContactHelper
	{

		#region Constructors

		public ContactHelper() { }

		#endregion

		#region Public Access Methods

		/// <summary>
		/// Get Contact by ID
		/// </summary>
		/// <param name="ID">ID of the Contact</param>
		/// <returns></returns>
		public Task<Contact> GetContactAsync(int ID)
		{
			return Task.Run(() =>
			{
				return Contact.ExecuteCreate(ID);
			});
		}

		/// <summary>
		/// Get Contact List
		/// </summary>
		/// <returns></returns>
		public Task<List<Contact>> GetContactListAsync()
		{
			return Task.Run(() =>
			{
				return Contact.List();
			});
		}

		/// <summary>
		/// Get Contacts by read status
		/// </summary>
		/// <param name="ReadStatus">Read Status</param>
		/// <returns></returns>
		public Task<List<Contact>> GetContactListByReadStatusAsync(int ReadStatus)
		{
			return Task.Run(() =>
			{
				return Contact.ListByReadStatus(ReadStatus);
			});
		}

		/// <summary>
		/// Create Contact
		/// </summary>
		/// <param name="Name"></param>
		/// <param name="Email"></param>
		/// <param name="ContactNo"></param>
		/// <param name="Subject"></param>
		/// <param name="Message"></param>
		/// <param name="ReadStatus"></param>
		/// <returns></returns>
		public Task<bool> CreateContactAsync(
			string Name,
			string Email,
			string ContactNo,
			string Subject,
			string Message,
			int ReadStatus)
		{
			return Task.Run(() =>
			{
				Contact             contact             = Contact.ExecuteCreate(Name, Email, ContactNo, Subject, Message, ReadStatus);
				contact.Insert();

				return (contact.ID != -1) ? true : false;
			});
		}

		/// <summary>
		/// Changes Read status of Contact by ID
		/// </summary>
		/// <param name="ID">ID of the Contact</param>
		/// <param name="ReadStatus">New Read Status</param>
		/// <returns></returns>
		public Task<bool> ChangeReadStatusAsync(int ID, int ReadStatus)
		{
			return Task.Run(() =>
			{
				Contact             contact             = Contact.ExecuteCreate(ID);
				contact.UpdateReadStatus(ReadStatus);

				return (contact.ReadStatus == ReadStatus) ? true : false;
			});
		}

		#endregion

	}
}
