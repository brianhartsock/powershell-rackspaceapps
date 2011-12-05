using System;
using System.Net;

namespace EmailApiSnapIn
{
    /// <summary>
    /// Exception for Api requests that fail due to some web request failure
    /// </summary>
    public class ApiException : Exception
    {
        const string DefaultErrorMessage = "Error communicating with the API server";
        const string ErrorMessageHeaderKey = "x-error-message";

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiException"/> class.
        /// </summary>
        /// <param name="innerException">The inner exception.</param>
        public ApiException(WebException innerException)
            : base(GetMessageString(innerException), innerException)
        { }

        private static string GetMessageString(WebException ex)
        {
            if (ex == null)
            {
                return DefaultErrorMessage;
            }
            else if (ex.Response == null)
            {
                return ex.Message;
            }
            else
            {
                return ex.Response.Headers[ErrorMessageHeaderKey] ?? ex.Message;
            }
        }
    }
}
