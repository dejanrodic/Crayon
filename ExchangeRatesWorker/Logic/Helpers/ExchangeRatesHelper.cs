using ExchangeRatesWorker.Models;
using ExchangeRatesWorker.Models.ExchangeratesApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExchangeRatesWorker.Logic.Helpers
{
    public static class ExchangeRatesHelper
    {
        public static ExchangeRatesInfo TransformToExchangeRatesInfo(this IEnumerable<HistoryItemExRatesApi> input)
        {
            var orderBy = input.OrderBy(r => r.Rate);

            var min = orderBy.First();
            var max = orderBy.Last();

            var minExRateItem = new ExchangeRateItem { Date = min.Date.ToString(), Rate = string.Format("{0:0.0000000000}", min.Rate) };
            var maxExRateItem = new ExchangeRateItem { Date = max.Date.ToString(), Rate = string.Format("{0:0.0000000000}", max.Rate) };

            return new ExchangeRatesInfo { MinimumExchangeRateItem = minExRateItem, MaximumExchangeRateItem = maxExRateItem, AverageRate = string.Format("{0:0.0000000000}", input.Average(r => r.Rate)) };
        }
    }
}
