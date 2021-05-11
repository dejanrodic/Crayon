using ExchangeRatesWorker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExchangeRatesWorker.Logic.RemoteServices.ExchangeratesApi
{
    public interface IExchangeRatesService : IGetRatesInfo
    {
        Task<Dictionary<string, string>> GetSupportedSymbols();
    }
}
