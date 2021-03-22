using StooqApiClient.DTO;
using TinyCsvParser.Mapping;
using TinyCsvParser.Model;

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

            MapUsing(VolumeMapper);
        }

        private bool VolumeMapper(Candle inProgressEntity, TokenizedRow rowData)
        {
            if (rowData.Tokens.Length == 6 && uint.TryParse(rowData.Tokens[5], out var volume))
                inProgressEntity.Volume = volume;

            return true;
        }
    }
}
