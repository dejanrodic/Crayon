using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExchangeRatesWorker.Models.ExchangeratesApi
{
    public class HistoryResponseExRatesApi
    {
        public Dictionary<string, decimal> Rates { get; set; }
    }
}
