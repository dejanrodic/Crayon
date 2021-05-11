using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ExchangeRatesWorker.Exceptions
{
    public class InputMissingException : BaseExchangeRatesWorkerException
    {
        public InputMissingException(string paramName)
        : base($"Parameter {paramName} is missing.")
        {
            Statuscode = (int)HttpStatusCode.BadRequest;
        }
    }
}
