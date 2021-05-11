using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExchangeRatesWorker.Models.Api.Exchangerate
{
    public class SymbolsApiExchangerate
    {
        public Dictionary<string, Dictionary<string, string>> Symbols { get; set; }
    }
}
