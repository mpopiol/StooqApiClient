using System;

namespace StooqApiClient.DTO
{
    public class Candle
    {
        public DateTime Date { get; set; }
        public decimal Open { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal Close { get; set; }
        public uint? Volume { get; set; }
    }
}
