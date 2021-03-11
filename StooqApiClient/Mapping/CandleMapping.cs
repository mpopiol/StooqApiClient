using StooqApiClient.DTO;
using TinyCsvParser.Mapping;

namespace StooqApiClient.Mapping
{
    internal class CandleMapping : CsvMapping<Candle>
    {
        internal CandleMapping() : base()
        {
            MapProperty(0, x => x.Date);
            MapProperty(1, x => x.Open);
            MapProperty(2, x => x.High);
            MapProperty(3, x => x.Low);
            MapProperty(4, x => x.Close);
            MapProperty(5, x => x.Volume);
        }
    }
}
