using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Tables.Content.Helpers
{
	public class ContactHelper
	{
		public ContactHelper() { }

		public Task<Contact> GetContactAsync(int ID)
		{
			return Task.Run(() =>
			{
				return Contact.ExecuteCreate(ID);
			});
		}

		public Task<List<Contact>> GetContactListAsync()
		{
			return Task.Run(() =>
			{
				return Contact.List();
			});
		}

		public Task<List<Contact>> GetContactListByReadStatusAsync(int ReadStatus)
		{
			return Task.Run(() =>
			{
				return Contact.ListByReadStatus(ReadStatus);
			});
		}

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

		public Task<bool> ChangeReadStatusAsync(int ID, int ReadStatus)
		{
			return Task.Run(() =>
			{
				Contact             contact             = Contact.ExecuteCreate(ID);
				contact.UpdateReadStatus(ReadStatus);

				return (contact.ReadStatus == ReadStatus) ? true : false;
			});
		}
	}
}
