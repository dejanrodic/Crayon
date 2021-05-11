using ExchangeRatesWorker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExchangeRatesWorker.Logic.RemoteServices.Api.Exchangerate
{
    public interface IApiExchangeRateService : IGetRatesInfo
    {
        Task<Dictionary<string, Dictionary<string, string>>> GetSupportedSymbols();
    }
}
