using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExchangeRatesWorker.Models.Api.Exchangerate
{
    public class TimeseriesResponseApiExchangerate
    {
        public Dictionary<string, Dictionary<string, decimal>> Rates { get; set; }

    }
}
