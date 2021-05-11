using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExchangeRatesWorker.Models.ExchangeratesApi
{
    public class HistoryItemExRatesApi
    {
        public string Date { get; set; }
        public decimal Rate { get; set; }
        public string BaseCurrency { get; set; }
        public string TargetCurrency { get; set; }
    }
}
