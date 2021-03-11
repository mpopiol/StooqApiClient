using System;
using System.Net;

namespace StooqApiClient.Exceptions
{
    internal class StooqApiException : Exception
    {
        public HttpStatusCode StatusCode { get; }

        internal StooqApiException(HttpStatusCode httpStatusCode)
            : base ($"Error occured on api connection: {httpStatusCode} STATUS CODE. Ensure that correct {nameof(Stooq.ApiBaseAddress)} is used")
        {
            StatusCode = httpStatusCode;
        }
    }
}
