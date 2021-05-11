using ExchangeRatesWorker.Controllers;
using ExchangeRatesWorker.Exceptions;
using ExchangeRatesWorker.Logic.RemoteServices.ExchangeratesApi;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExchangeRatesWorker.Logic.Helpers
{
    public class CurrencySymbolsExRatesApiHelper : ICurrencySymbolsExRatesApiHelper
    {
        private IExchangeRatesService _exchangeRatesService;
        private Dictionary<string, string> symbols;

        private async Task<Dictionary<string, string>> GetSupportedSymbolsDictionary()
        {
            if (symbols == null && _exchangeRatesService != null)
            {
                symbols = await _exchangeRatesService.GetSupportedSymbols();
            }

            return symbols;
        }
        private async Task<IEnumerable<string>> GetSupportedSymbols()
        {
            return (await GetSupportedSymbolsDictionary())?.Keys;
        }
        public async Task<bool> ValidateSymbols(IExchangeRatesService exchangeRatesService, params string[] keys)
        {
            _exchangeRatesService = exchangeRatesService;

            var symbols = await GetSupportedSymbols();
            var invalidSymbols = keys.Where(k => !symbols.Contains(k.ToUpper()));

            if (invalidSymbols.Any())
            {
                var dict = GetSupportedSymbolsDictionary();

                throw new InputFormatArgumentException("Currency symbol", $"{ string.Join(",", invalidSymbols)}. Supported symbols: {string.Join(",", symbols)}");
            }

            return true;
        }
    }
}
