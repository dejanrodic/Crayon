using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ExchangeRatesWorker.Logic.Helpers;
using ExchangeRatesWorker.Exceptions;

namespace ExchangeRatesWorker.Models
{
    [ExchangeRatesParamsValidator(minimumDate: "1999-01-01")]
    public class ExchangeRatesParams
    {
        [Required(ErrorMessage = "A value for the 'Dates' parameter was not provided.")]
        public string Dates { get; set; }

        [Required(ErrorMessage = "A value for the 'BaseCurrency' parameter was not provided.")]
        public string BaseCurrency { get; set; }

        [Required(ErrorMessage = "A value for the 'TargetCurrency' parameter was not provided.")]
        public string TargetCurrency { get; set; }
    }

    public class ExchangeRatesParamsValidator : ValidationAttribute
    {
        private DateTime _minimumDate;

        public ExchangeRatesParamsValidator(string minimumDate = "1990-01-01")
        {

            _minimumDate = minimumDate.TransformToDates().First();
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var exchangeRatesParams = (ExchangeRatesParams)validationContext.ObjectInstance;

            var dates = exchangeRatesParams.Dates.TransformToDates();
            if (dates.Any(d => d < _minimumDate || d > DateTime.Today))
            {
                throw new InputFormatArgumentException("date", $"all dates must be in range from {_minimumDate.ToFormattedString()} to {DateTime.Today.ToFormattedString()}");
            }

            return ValidationResult.Success;
        }
    }
}
