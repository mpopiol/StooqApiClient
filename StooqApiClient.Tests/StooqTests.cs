using FluentAssertions;
using StooqApiClient.DTO;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace StooqApiClient.Tests
{
    public class StooqTests
    {
        [Theory]
        [InlineData("cdr")] //stock
        [InlineData("^spx")] //index
        [InlineData("hkdusd")] //currency
        public async Task GetHistoricalDataAsync_TickerOnly_ReturnData(string ticker)
        {
            var testRequest = new StooqRequest
            {
                Ticker = ticker
            };
            var result = await Stooq.GetHistoricalDataAsync(testRequest);

            result.Status.Should().Be(HttpStatusCode.OK);
            result.Results.Should().NotBeEmpty();
        }

        [Theory]
        [InlineData("cdr", "31.12.2020")]
        [InlineData("^spx", "30.12.2019")]
        [InlineData("hkdusd", "30.11.2020")]
        public async Task GetHistoricalDataAsync_TickerAndFromDate_ReturnData(string ticker, string fromDateString)
        {
            var fromDate = DateTime.Parse(fromDateString);
            var testRequest = new StooqRequest
            {
                Ticker = ticker,
                FromDate = fromDate
            };
            var result = await Stooq.GetHistoricalDataAsync(testRequest);

            result.Status.Should().Be(HttpStatusCode.OK);
            result.Results.Min(candle => candle.Date).Should().BeAfter(fromDate.AddMilliseconds(-1));
        }

        [Theory]
        [InlineData("cdr", "31.12.2020")]
        [InlineData("^spx", "30.12.2019")]
        [InlineData("hkdusd", "30.11.2020")]
        public async Task GetHistoricalDataAsync_TickerAndToDate_ReturnData(string ticker, string toDateString)
        {
            var toDate = DateTime.Parse(toDateString);
            var testRequest = new StooqRequest
            {
                Ticker = ticker,
                ToDate = toDate
            };
            var result = await Stooq.GetHistoricalDataAsync(testRequest);

            result.Status.Should().Be(HttpStatusCode.OK);
            result.Results.Max(candle => candle.Date).Should().BeBefore(toDate.AddMilliseconds(1));
        }
    }
}