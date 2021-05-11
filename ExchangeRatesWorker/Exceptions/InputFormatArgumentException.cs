using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ExchangeRatesWorker.Exceptions
{
    public class InputFormatArgumentException : BaseExchangeRatesWorkerException
    {
        public InputFormatArgumentException(string paramName, string message)
        : base($"Invalid parameter input for {paramName}: {message}.")
        {
            Statuscode = (int)HttpStatusCode.BadRequest;
        }
    }
}
