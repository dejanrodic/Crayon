using ExchangeRatesWorker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExchangeRatesWorker.Logic.RemoteServices
{
    public interface IGetRatesInfo
    {
        Task<ExchangeRatesInfo> GetRatesInfo(ExchangeRatesParams exchangeRatesInfo);
    }
}
