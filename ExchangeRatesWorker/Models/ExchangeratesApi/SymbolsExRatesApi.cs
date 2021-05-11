using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExchangeRatesWorker.Models.ExchangeratesApi
{
    public class SymbolsExRatesApi
    {
        public string Success { get; set; }
        public Dictionary<string, string> Symbols { get; set; }
    }
}
