using FluentAssertions;
using StooqApiClient.DTO;
using StooqApiClient.Enums;
using System;
using Xunit;

namespace StooqApiClient.Tests
{
    public class StooqRequestParserTests
    {
        [Fact]
        public void GetUri_TickerOnlySet_DefaultValuesForOtherParams()
        {
            var testRequest = new StooqRequest
            {
                Ticker = "testTicker"
            };
            var result = StooqRequestParser.GetUri(string.Empty, testRequest);

            result.Should().Contain($"?s={testRequest.Ticker}");

            result.Should().Contain("&i=d");
            result.Should().Contain("&o=0000000");
            result.Should().NotContain("&d1=");
            result.Should().NotContain("&d2=");
        }

        [Fact]
        public void GetUri_FromDateSet_FromAndToDateSetInUri()
        {
            var testFromDate = DateTime.Today.AddDays(-30);
            var testRequest = new StooqRequest
            {
                Ticker = "testTicker",
                FromDate = testFromDate
            };
            var result = StooqRequestParser.GetUri(string.Empty, testRequest);

            result.Should().Contain($"?s={testRequest.Ticker}");

            result.Should().Contain("&i=d");
            result.Should().Contain("&o=0000000");
            result.Should().Contain($"&d1={testFromDate:yyyyMMdd}");
            result.Should().Contain($"&d2={DateTime.MaxValue:yyyyMMdd}");
        }

        [Fact]
        public void GetUri_ToDateSet_FromAndToDateSetInUri()
        {
            var testToDate = DateTime.Today.AddDays(-30);
            var testRequest = new StooqRequest
            {
                Ticker = "testTicker",
                ToDate = testToDate
            };
            var result = StooqRequestParser.GetUri(string.Empty, testRequest);

            result.Should().Contain($"?s={testRequest.Ticker}");

            result.Should().Contain("&i=d");
            result.Should().Contain("&o=0000000");
            result.Should().Contain($"&d1={DateTime.MinValue:yyyyMMdd}");
            result.Should().Contain($"&d2={testToDate:yyyyMMdd}");
        }

        [Fact]
        public void GetUri_SkipDividendsAndOthers_ReturnProperlyFormattedStringAsUriParameter()
        {
            var skip = Skip.Dividends | Skip.Others;
            var testRequest = new StooqRequest
            {
                Ticker = "testTicker",
                Skip = skip
            };
            var result = StooqRequestParser.GetUri(string.Empty, testRequest);

            result.Should().Contain("&o=0100001");
        }

        [Fact]
        public void GetUri_YearlyInterval_ReturnYForIntervalParameterValue()
        {
            var interval = Interval.Yearly;
            var testRequest = new StooqRequest
            {
                Ticker = "testTicker",
                Interval = interval
            };
            var result = StooqRequestParser.GetUri(string.Empty, testRequest);

            result.Should().Contain("&i=y");
        }

        [Fact]
        public void GetUri_QuaterlyInterval_ReturnQForIntervalParameterValue()
        {
            var interval = Interval.Quaterly;
            var testRequest = new StooqRequest
            {
                Ticker = "testTicker",
                Interval = interval
            };
            var result = StooqRequestParser.GetUri(string.Empty, testRequest);

            result.Should().Contain("&i=q");
        }

        [Fact]
        public void GetUri_MonthlyInterval_ReturnMForIntervalParameterValue()
        {
            var interval = Interval.Monthly;
            var testRequest = new StooqRequest
            {
                Ticker = "testTicker",
                Interval = interval
            };
            var result = StooqRequestParser.GetUri(string.Empty, testRequest);

            result.Should().Contain("&i=m");
        }

        [Fact]
        public void GetUri_WeeklyInterval_ReturnWForIntervalParameterValue()
        {
            var interval = Interval.Weekly;
            var testRequest = new StooqRequest
            {
                Ticker = "testTicker",
                Interval = interval
            };
            var result = StooqRequestParser.GetUri(string.Empty, testRequest);

            result.Should().Contain("&i=w");
        }
    }
}
