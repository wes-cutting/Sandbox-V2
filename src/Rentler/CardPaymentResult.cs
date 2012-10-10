using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rentler
{
	public class CardPaymentResult
	{
		public string TransactionId { get; set; }
		public string AuthString { get; set; }
		public string MessageText { get; set; }
		public bool Approved { get; set; }
		public bool ServiceUnavailable { get; set; }
	}
}
