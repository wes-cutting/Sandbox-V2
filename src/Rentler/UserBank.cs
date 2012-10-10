using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Rentler
{
	public class UserBank
	{
		public int UserBankId { get; set; }
		
		public int UserId { get; set; }

		public long? PayerAlias { get; set; }
		
		public long? PayeeAlias { get; set; }

		[Required, StringLength(50)]
		public string AccountName { get; set; }

		[Required, StringLength(9)]
		public string RoutingNumber { get; set; }

		[Required, StringLength(20)]
		public string AccountNumber { get; set; }

		[Required, StringLength(20)]
		public string AccountType { get; set; }

		[Required, StringLength(50)]
		public string FirstName { get; set; }

		[Required, StringLength(50)]
		public string LastName { get; set; }

		[Required, StringLength(50)]
		public string Address1 { get; set; }

		[StringLength(50)]
		public string Address2 { get; set; }

		[Required, StringLength(50)]
		public string City { get; set; }

		[Required, StringLength(2)]
		public string State { get; set; }

		[Required, StringLength(10)]
		public string Zip { get; set; }

		[StringLength(20)]
		public string Phone { get; set; }

		[StringLength(50)]
		public string Email { get; set; }

		[Required]
		public bool IsDeleted { get; set; }

		public DateTime UpdateDate { get; set; }

		public string UpdatedBy { get; set; }

		[Required]
		public DateTime CreateDate { get; set; }

		[Required, StringLength(100)]
		public string CreatedBy { get; set; }
	}
}
