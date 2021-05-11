using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExchangeRatesWorker.Settings
{
    public class SwaggerSettings
    {
        public string Title { get; set; }
        public string JsonRoute { get; set; }
        public string Description { get; set; }
        public string UiEndpoint { get; set; }
        public string ContactName { get; set; }
        public string ContactUrl { get; set; }
    }
}
