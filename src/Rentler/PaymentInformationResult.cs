using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rentler
{
	public class PaymentInformationResult
	{
		public bool Paid { get; set; }
		public bool Active { get; set; }
		public string ErrorMessage { get; set; }
	}
}
