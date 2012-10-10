using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewNewRentler
{
	public static class TimeSpanExtensions
	{
		public static string ToPrettyString(this TimeSpan span)
		{
			if(span.Days > 0)
				return string.Format("{0} days, {1} hours, {2} minutes", span.Days, span.Hours, span.Minutes);
			if(span.Hours > 0)
				return string.Format("{0} hours, {1} minutes", span.Hours, span.Minutes);
			if(span.Minutes > 0)
				return string.Format("{0} minutes, {1} seconds", span.Minutes, span.Seconds);
			if(span.Seconds > 0)
				return string.Format("{0} seconds, {1}ms", span.Seconds, span.Milliseconds);
			if(span.Milliseconds > 0)
				return string.Format("{0}ms", span.Milliseconds);

			return string.Format("{0} seconds", span.TotalSeconds.ToString("F"));
		}
	}
}
