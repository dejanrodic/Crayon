using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExchangeRatesWorker.Models
{
    public class ExchangeRatesInfo
    {
        public ExchangeRateItem MinimumExchangeRateItem { get; set; }
        public ExchangeRateItem MaximumExchangeRateItem { get; set; }
        public string AverageRate { get; set; }

    }
}
