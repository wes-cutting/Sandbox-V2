using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Rentler
{
    /// <summary>
    /// Represents a generic api status result.
    /// </summary>
    public class Status<T> : Status
    {
        /// <summary>
        /// Gets or sets the result.
        /// </summary>
        /// <value>The result.</value>
        public T Result { get; set; }
    }

    /// <summary>
    /// Represents a status result.
    /// </summary>
    public class Status
    {
        /// <summary>
        /// Gets or sets the status code.
        /// </summary>
        /// <value>The status code.</value>
        public int StatusCode { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the validation errors.
        /// </summary>
        /// <value>The validation errors.</value>
        public ValidationResult[] Errors { get; set; }

        /// <summary>
        /// Returns an OK status with the given result embeded.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <returns></returns>
        public static Status<T> OK<T>(T result)
        {
            return new Status<T>()
            {
                StatusCode = 200,
                Message = "OK",
                Result = result
            };
        }

        public static Status<T> UnAuthorized<T>()
        {
            return new Status<T>()
            {
                StatusCode = 403,
                Message = "Unauthorized"
            };
        }

        public static Status<T> NotFound<T>()
        {
            return new Status<T>()
            {
                StatusCode = 404,
                Message = "Not Found"
            };
        }

        /// <summary>
        /// Errors the specified message.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message">The message.</param>
        /// <param name="result">The result.</param>
        /// <returns></returns>
        public static Status<T> Error<T>(string message, T result)
        {
            return new Status<T>()
            {
                StatusCode = 500,
                Message = message,
                Result = result
            };
        }

        public static Status<T> Validatate<T>(T item)
        {
            List<ValidationResult> validationResults = new List<ValidationResult>();
            ValidationContext context = new ValidationContext(item, null, null);
            bool validated = Validator.TryValidateObject(item, context, validationResults);
            if (!validated)
                return Status.Error<T>("Failed to validate", item, validationResults);
            return Status.OK<T>(item);
        }

        public static Status<T> ValidationError<T>(T item, string memberName, string error)
        {
            List<ValidationResult> results = new List<ValidationResult>();
            List<string> memberNames = new List<string>();
            memberNames.Add(memberName);
            results.Add(new ValidationResult(error, memberNames.ToArray()));

            return new Status<T>()
            {
                StatusCode = 500,
                Message = error,
                Result = item,
                Errors = results.ToArray()
            };
        }

        /// <summary>
        /// Errors the specified message.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message">The message.</param>
        /// <param name="result">The result.</param>
        /// <returns></returns>
        public static Status<T> Error<T>(string message, T result, List<ValidationResult> errors)
        {
            return new Status<T>()
            {
                StatusCode = 500,
                Message = message,
                Result = result,
                Errors = errors.ToArray()
            };
        }
    }
}
