using StooqApiClient.DTO;
using StooqApiClient.Exceptions;
using StooqApiClient.Mapping;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TinyCsvParser;

namespace StooqApiClient
{
    public static class Stooq
    {
        public static string ApiBaseAddress = "https://stooq.com/q/d/l";

        private static readonly HttpClient httpClient = new HttpClient();
        private static readonly CsvParser<Candle> csvParser = new CsvParser<Candle>(
            new CsvParserOptions(true, ','), 
            new CandleMapping());

        public static async Task<StooqResponse> GetHistoricalDataAsync(StooqRequest request)
        {
            try
            {
                var responseStream = await GetResponseStreamAsync(request).ConfigureAwait(false);

                return GetParsedResponse(responseStream);
            }
            catch (StooqApiException exception)
            {
                return new StooqResponse
                {
                    Status = exception.StatusCode,
                    ErrorMessage = exception.Message
                };
            }
        }

        public static StooqResponse GetHistoricalData(StooqRequest request) => GetHistoricalDataAsync(request).Result;

        private static async Task<Stream> GetResponseStreamAsync(StooqRequest request)
        {
            var uri = StooqRequestParser.GetUri(ApiBaseAddress, request);

            var response = await httpClient.GetAsync(uri);

            if (!response.IsSuccessStatusCode)
                throw new StooqApiException(response.StatusCode);

            return await response.Content.ReadAsStreamAsync();
        }

        private static StooqResponse GetParsedResponse(Stream responseStream)
        {
            var candles = csvParser.ReadFromStream(responseStream, Encoding.UTF8).ToArray();

            return new StooqResponse
            {
                Status = HttpStatusCode.OK,
                Results = candles.Select(candle => candle.Result).ToArray()
            };
        }

    }
}
