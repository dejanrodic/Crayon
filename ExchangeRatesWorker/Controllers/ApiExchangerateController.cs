using System.Threading.Tasks;
using ExchangeRatesWorker.Logic.RemoteServices.Api.Exchangerate;
using ExchangeRatesWorker.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExchangeRatesWorker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiExchangerateController : ControllerBase
    {
        private readonly IApiExchangeRateService _apiExchangeRatesService;
        public ApiExchangerateController(IApiExchangeRateService apiExchangeRatesService)
        {
            _apiExchangeRatesService = apiExchangeRatesService;
        }
        
        [HttpGet("GetExchangeRatesInfo")]
        public async Task<ActionResult> GetExchangeRatesInfo([FromQuery]ExchangeRatesParams exchangeRatesInfo)
        {
            var result = await _apiExchangeRatesService.GetRatesInfo(exchangeRatesInfo);

            return Ok(result);
        }
        
        [HttpGet("GetSupportedSymbols")]
        public async Task<ActionResult<string>> GetSupportedSymbols()
        {
            return Ok(await _apiExchangeRatesService.GetSupportedSymbols());
        }
    }
}