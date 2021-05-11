using ExchangeRatesWorker.Logic.RemoteServices.ExchangeratesApi;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExchangeRatesWorker.Logic.Helpers
{
    public interface ICurrencySymbolsExRatesApiHelper
    {
        //Task<IEnumerable<string>> GetSupportedSymbols(IExchangeRatesService exchangeRatesService);
        //Task<Dictionary<string, string>> GetSupportedSymbolsDictionary(IExchangeRatesService exchangeRatesService);
        Task<bool> ValidateSymbols(IExchangeRatesService exchangeRatesService, params string[] keys);
    }
}