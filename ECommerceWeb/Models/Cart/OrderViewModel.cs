using ECommerceWeb.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ETAH = ECommerce.Tables.Active.HR;
using ETC = ECommerce.Tables.Content;

namespace ECommerceWeb.Models.Cart
{
	public class OrderViewModel
	{

		#region Members

		private int                 id                      = Constants.DEFAULT_VALUE_INT;
		private int                 accountID               = Constants.DEFAULT_VALUE_INT;
		private DateTime            dateCreated             = DateTime.MinValue;
		private int                 status                  = Constants.DEFAULT_VALUE_INT;
		private int                 paymentMethod           = Constants.DEFAULT_VALUE_INT;
		private decimal             totalAmount             = Constants.DEFAULT_VALUE_DECIMAL;
		private ETC.Order           order                   = null;
		private List<ETC.OrderItem> orderItems              = null;
		private ETAH.Account        user                    = null;

		#endregion

		#region Properties

		public int ID
		{
			get { return this.id; }
			set { this.id = value; }
		}

		public int AccountID
		{
			get { return this.accountID; }
			set { this.accountID = value; }
		}

		public DateTime DateCreated
		{
			get { return this.dateCreated; }
			set { this.dateCreated = value; }
		}

		public int Status
		{
			get { return this.status; }
			set { this.status = value; }
		}

		public int PaymentMethod
		{
			get { return this.paymentMethod; }
			set { this.paymentMethod = value; }
		}

		public decimal TotalAmount
		{
			get { return this.totalAmount; }
			set { this.totalAmount = value; }
		}

		public ETC.Order Order
		{
			get { return this.order; }
			set { this.order = value; }
		}

		public List<ETC.OrderItem> OrderItems
		{
			get { return this.orderItems; }
			set { this.orderItems = value; }
		}

		public ETAH.Account User
		{
			get { return this.user; }
			set { this.user = value; }
		}

		#endregion

		#region Constructors

		public OrderViewModel()
		{
			this.CheckPendingOrders();

			if (this.order != null)
			{
				this.id                 = this.order.ID;
				this.accountID          = this.order.AccountID;
				this.dateCreated        = this.order.DateCreated;
				this.status             = this.order.Status;
				this.paymentMethod      = this.order.PaymentMethod;
				this.totalAmount        = this.order.TotalAmount;
				this.orderItems         = ETC.OrderItem.ListByOrderID(this.id);
				this.user               = this.order.ExecuteCreateAccountByAccountID();
			}
		}

		public OrderViewModel(int OrderID)
		{
			this.order                  = ETC.Order.ExecuteCreate(OrderID);

			if (this.order != null)
			{
				this.id                 = this.order.ID;
				this.accountID          = this.order.AccountID;
				this.dateCreated        = this.order.DateCreated;
				this.status             = this.order.Status;
				this.paymentMethod      = this.order.PaymentMethod;
				this.totalAmount        = this.order.TotalAmount;
				this.orderItems         = ETC.OrderItem.ListByOrderID(this.id);
				this.user               = this.order.ExecuteCreateAccountByAccountID();
			}
		}

		#endregion

		#region Methods

		private void CheckPendingOrders()
		{
			List<ETC.Order>             orders                              = ETC.Order.ListByAccountID(Common.Session.Account.ID);
			ETC.Order                   order                               = null;

			foreach (ETC.Order _order in orders)
			{
				if (_order.Status == ETC.Order.STATUS_PENDING)
				{
					order                                                   = _order;
					break;
				}
			}

			if (order == null)
			{
				order                                                       = ETC.Order.ExecuteCreate(Common.Session.Account.ID, ETC.Order.STATUS_PENDING, ETC.Order.PAYMENT_METHOD_DEFAULT, Constants.DEFAULT_VALUE_DECIMAL);
				order.Insert();
			}

			if (order.ID != Constants.DEFAULT_VALUE_INT)
			{
				Common.Session.CurrentOrderID                               = order.ID;
				this.order                                                  = order;
			}
		}

		public Task<bool> CheckOut()
		{
			return Task.Run(() =>
			{
				bool                result                  = false;

				if (this.order != null)
				{
					order.Update(DateTime.Now, ETC.Order.STATUS_COMPLETED, this.order.PaymentMethod, this.order.TotalAmount);
					result                                  = true;
				}

				return result;
			});
		}

		public static Task<bool> RemoveOrder(int? orderID)
		{
			return Task.Run(() =>
			{
				bool					result                  = false;

				if (orderID != null)
				{
					ETC.Order           order                   = ETC.Order.ExecuteCreate(orderID ?? 0);
					order.Delete();

					result                                      = true;
				}

				return result;
			});
		}

		#endregion

	}
}