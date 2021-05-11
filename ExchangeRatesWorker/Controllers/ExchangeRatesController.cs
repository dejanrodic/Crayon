using System.Threading.Tasks;
using ExchangeRatesWorker.Logic.RemoteServices.ExchangeratesApi;
using ExchangeRatesWorker.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExchangeRatesWorker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExchangeRatesController : ControllerBase
    {
        private readonly IExchangeRatesService _exchangeRatesService;
        public ExchangeRatesController(IExchangeRatesService exchangeRatesService)
        {
            _exchangeRatesService = exchangeRatesService;
        }

        [HttpGet("GetExchangeRatesInfo")]
        public async Task<ActionResult> GetExchangeRatesInfo([FromQuery]ExchangeRatesParams exchangeRatesInfo)
        {
            var result = await _exchangeRatesService.GetRatesInfo(exchangeRatesInfo);

            return Ok(result);
        }

        [HttpGet("GetSupportedSymbols")]
        public async Task<ActionResult> GetSupportedSymbols()
        {
            return Ok(await _exchangeRatesService.GetSupportedSymbols());
        }       
    }
}
