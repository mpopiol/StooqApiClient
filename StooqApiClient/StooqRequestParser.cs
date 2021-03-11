using StooqApiClient.DTO;
using System;
using System.Runtime.CompilerServices;
using System.Text;

[assembly: InternalsVisibleTo("StooqApiClient.Tests")]
namespace StooqApiClient
{
    internal static class StooqRequestParser
    {
        private const string tickerSymbol = "s";
        private const string fromDateSymbol = "d1";
        private const string toDateSymbol = "d2";
        private const string intervalSymbol = "i";
        private const string skipSymbol = "o";

        internal static string GetUri(string baseAddress, StooqRequest request)
        {
            var sb = new StringBuilder(baseAddress);

            sb.Append($"?{tickerSymbol}={request.Ticker}");

            if (request.FromDate.HasValue || request.ToDate.HasValue)
            {
                sb.Append($"&{fromDateSymbol}={(request.FromDate ?? DateTime.MinValue):yyyyMMdd}");

                sb.Append($"&{toDateSymbol}={(request.ToDate ?? DateTime.MaxValue):yyyyMMdd}");
            }

            var interval = Convert.ToChar((int)request.Interval);
            sb.Append($"&{intervalSymbol}={interval}");

            var skip = Convert.ToString((int)request.Skip, 2).PadLeft(7, '0');
            sb.Append($"&{skipSymbol}={skip}");

            return sb.ToString();
        }
    }
}
