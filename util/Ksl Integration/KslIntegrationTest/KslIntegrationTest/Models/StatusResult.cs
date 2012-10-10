using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KslIntegrationTest.Models
{
	public class StatusResult<T>
	{
		public StatusResult() { }

		public StatusResult(T result)
			: this(true, "", result)
		{
			this.Result = result;
		}

		public StatusResult(bool success, string message, T result)
		{
			this.Success = success;
			this.Message = message;
			this.Result = result;
		}

		public bool Success { get; set; }

		public string Message { get; set; }

		public int ErrorCode { get; set; }

		public T Result { get; set; }
	}
}