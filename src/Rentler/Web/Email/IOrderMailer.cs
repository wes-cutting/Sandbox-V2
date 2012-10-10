using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rentler.Web.Email
{
	public interface IOrderMailer
	{
		Status<bool> Receipt(EmailOrderReceiptModel model);
	}

	public class EmailOrderReceiptModel
	{
		public string To { get; set; }
		public string Name { get; set; }
        public long BuildingId { get; set; }
        public decimal OrderTotal { get; set; }
        public List<OrderItem> OrderItems { get; set; }
	}
}
