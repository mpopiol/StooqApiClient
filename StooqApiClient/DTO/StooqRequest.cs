using StooqApiClient.Enums;
using System;

namespace StooqApiClient.DTO
{
    public class StooqRequest
    {
        public string Ticker { get; set; }
        public Interval Interval { get; set; } = Interval.Daily;
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public Skip Skip { get; set; } = Skip.None;
    }
}
