using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rentler
{
	public class SinglePaymentResult
	{
		public long TransactionId { get; set; }
		public bool Accepted { get; set; }
		public bool ServiceUnavailable { get; set; }
		public string ErrorMessage { get; set; }
	}
}
