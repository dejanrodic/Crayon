using ExchangeRatesWorker.Exceptions;
using ExchangeRatesWorker.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ExchangeRatesWorker.Middlewares
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception e)
            {
                //todo implement logger
                await HandleExceptionAsync(httpContext,e);
            }
        }

        private Task HandleExceptionAsync(HttpContext httpContext, Exception e)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var message = "Internal server error";


            if (e is BaseExchangeRatesWorkerException)
            {
                var customException = e as BaseExchangeRatesWorkerException;

                httpContext.Response.StatusCode = customException.Statuscode;
                message = customException.Message;
            }
            else if (e is HttpRequestException)
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                message = $"From unsuccessful remote request: {e.Message}";
            }


            return httpContext.Response.WriteAsync(new ErrorDetails
            {
                StatusCode = httpContext.Response.StatusCode,
                Message = message
            }.ToString()); ;
        }
    }
}
