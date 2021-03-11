using System.Net;

namespace StooqApiClient.DTO
{
    public class StooqResponse
    {
        public HttpStatusCode Status { get; set; }
        public string ErrorMessage { get; set; }
        public Candle[] Results { get; set; }
    }
}
