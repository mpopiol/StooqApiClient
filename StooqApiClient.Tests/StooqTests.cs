using FluentAssertions;
using StooqApiClient.DTO;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace StooqApiClient.Tests
{
    public class StooqTests
    {
        [Fact]
        public async Task GetHistoricalDataAsync_CDRTickerOnly_ReturnData()
        {
            var testRequest = new StooqRequest
            {
                Ticker = "cdr"
            };
            var result = await Stooq.GetHistoricalDataAsync(testRequest);

            result.Status.Should().Be(HttpStatusCode.OK);
            result.Results.Should().NotBeEmpty();
        }
    }
}
