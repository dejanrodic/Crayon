using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExchangeRatesWorker.Exceptions
{
    public abstract class BaseExchangeRatesWorkerException : Exception
    {
        public int Statuscode { get; internal set; }
        public BaseExchangeRatesWorkerException(string message) :base(message) { }
    }
}
