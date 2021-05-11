using ExchangeRatesWorker.Logic.Helpers;
using ExchangeRatesWorker.Models;
using ExchangeRatesWorker.Models.Api.Exchangerate;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ExchangeRatesWorker.Logic.RemoteServices.Api.Exchangerate
{
    public class ApiExchangeRateService : IApiExchangeRateService
    {
        private readonly HttpClient _httpClient;

        public ApiExchangeRateService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ExchangeRatesInfo> GetRatesInfo(ExchangeRatesParams exchangeRatesInfo)
        {
            var dates = exchangeRatesInfo.Dates.TransformToDates();
            DateTime startDate = dates.Min();
            DateTime endDate = dates.Max();

            var uri = $"timeseries?start_date={startDate.ToFormattedString()}&end_date={endDate.ToFormattedString()}&base={exchangeRatesInfo.BaseCurrency.ToUpper()}&symbols={exchangeRatesInfo.TargetCurrency.ToUpper()}";
            var httpResponseMessage = await _httpClient.GetAsync(uri);
            var responseString = await httpResponseMessage.Content.ReadAsStringAsync();

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                throw new HttpRequestException(responseString);
            }

            var response = JsonConvert.DeserializeObject<TimeseriesResponseApiExchangerate>(responseString);

            return response.TransformToExchangeRatesInfo(dates);
        }
        public async Task<Dictionary<string, Dictionary<string, string>>> GetSupportedSymbols()
        {
            var uri = "symbols";

            var responseString = await _httpClient.GetStringAsync(uri);

            var output = JsonConvert.DeserializeObject<SymbolsApiExchangerate>(responseString);

            return output.Symbols;
        }
    }
}
