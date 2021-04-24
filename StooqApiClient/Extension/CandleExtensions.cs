using StooqApiClient.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace StooqApiClient.Extension
{
    public static class CandleExtensions
    {
        public static void MultiplyValues(this Candle candle, decimal ratio)
        {
            candle.Close *= ratio;
            candle.High *= ratio;
            candle.Low *= ratio;
            candle.Open *= ratio;
        }
    }
}
