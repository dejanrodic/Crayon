using ExchangeRatesWorker.Logic.Helpers;
using ExchangeRatesWorker.Models;
using ExchangeRatesWorker.Models.ExchangeratesApi;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ExchangeRatesWorker.Logic.RemoteServices.ExchangeratesApi
{
    public class ExchangeRatesService : IExchangeRatesService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ICurrencySymbolsExRatesApiHelper _currencySymbolsExRatesApiHelper;

        public ExchangeRatesService(HttpClient httpClient, IConfiguration configuration, ICurrencySymbolsExRatesApiHelper currencySymbolsExRatesApiHelper)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _currencySymbolsExRatesApiHelper = currencySymbolsExRatesApiHelper;

        }
        public async Task<ExchangeRatesInfo> GetRatesInfo(ExchangeRatesParams exchangeRatesInfo)
        {
            var dates = exchangeRatesInfo.Dates.TransformToDates();

            await _currencySymbolsExRatesApiHelper.ValidateSymbols(this, exchangeRatesInfo.BaseCurrency, exchangeRatesInfo.TargetCurrency);

            var values = new List<HistoryItemExRatesApi>();

            foreach (var date in dates)
            {
                var historyItem = await GetRatesOnDate(date.ToFormattedString(), exchangeRatesInfo.BaseCurrency, exchangeRatesInfo.TargetCurrency);
                values.Add(historyItem);
            }

            return values.TransformToExchangeRatesInfo();
        }

        public async Task<HistoryItemExRatesApi> GetRatesOnDate(string date, string baseCurrency, string targetCurrency)
        {
            var uri = $"{date}?access_key={_configuration["ExchangeAPIs:ExchangeratesApi:access_key"]}&symbols={baseCurrency},{targetCurrency}&format=1";
            var httpResponseMessage = await _httpClient.GetAsync(uri);
            var responseString = await httpResponseMessage.Content.ReadAsStringAsync();

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                throw new HttpRequestException(responseString);
            }

            var output = JsonConvert.DeserializeObject<HistoryResponseExRatesApi>(responseString);
            var exchangeRate = output.Rates[targetCurrency.ToUpper()] / output.Rates[baseCurrency.ToUpper()];

            return new HistoryItemExRatesApi { Date = date, BaseCurrency = baseCurrency, TargetCurrency = targetCurrency, Rate = exchangeRate };
        }

        public async Task<Dictionary<string, string>> GetSupportedSymbols()
        {
            var uri = $"symbols?access_key={_configuration["ExchangeAPIs:ExchangeratesApi:access_key"]}";

            var responseString = await _httpClient.GetStringAsync(uri);

            var output = JsonConvert.DeserializeObject<SymbolsExRatesApi>(responseString);

            return output.Symbols;
        }

    }
}
