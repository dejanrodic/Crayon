using ExchangeRatesWorker.Models;
using ExchangeRatesWorker.Models.Api.Exchangerate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExchangeRatesWorker.Logic.Helpers
{
    public static class ApiExchangeRateHelper
    {

        public static ExchangeRatesInfo TransformToExchangeRatesInfo(this TimeseriesResponseApiExchangerate input, IEnumerable<DateTime> requestedDates)
        {
            var requestedDateStrings = requestedDates.TransformToFormattedDateStrings();

            var requestedPairs = input.Rates.Select(pair => new { Date = pair.Key, Rate = pair.Value.First().Value }).Where(x => requestedDateStrings.Contains(x.Date)).ToList();

            var orderBy = requestedPairs.OrderBy(x => x.Rate);

            var minPair = orderBy.First();
            var maxPair = orderBy.Last();

            var minExRateItem = new ExchangeRateItem { Date = minPair.Date.ToString(), Rate = string.Format("{0:0.0000000000}", minPair.Rate) };
            var maxExRateItem = new ExchangeRateItem { Date = maxPair.Date.ToString(), Rate = string.Format("{0:0.0000000000}", maxPair.Rate)  };

            return new ExchangeRatesInfo { MinimumExchangeRateItem = minExRateItem, MaximumExchangeRateItem = maxExRateItem, AverageRate = string.Format("{0:0.0000000000}", requestedPairs.Average(r => r.Rate)) };
        }
    }
}
