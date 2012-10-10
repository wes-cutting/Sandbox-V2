using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Rentler.Web.Axioms
{
	/// <summary>
	/// Attribute allowing the validation of checkboxes. Both
	/// client and server-side examples are in the application.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
	public class MandatoryAttribute : ValidationAttribute, IClientValidatable
	{
		/// <summary>
		/// Determines whether the specified value is valid. It is valid if
		/// it is not null and it is a boolean and it is true. Useful for
		/// validating required checkboxes.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>
		/// 	<c>true</c> if the specified value is valid; otherwise, <c>false</c>.
		/// </returns>
		public override bool IsValid(object value)
		{
			return value != null && value is bool && (bool)value;
		}

		/// <summary>
		/// When implemented in a class, returns client validation rules for that class.
		/// </summary>
		/// <param name="metadata">The model metadata.</param>
		/// <param name="context">The controller context.</param>
		/// <returns>
		/// The client validation rules for this validator.
		/// </returns>
		public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
		{
			yield return new ModelClientValidationRule
			{
				ErrorMessage = FormatErrorMessage(metadata.DisplayName),
				ValidationType = "mandatory"
			};
		}
	}
}