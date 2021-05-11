using ExchangeRatesWorker.Exceptions;
using ExchangeRatesWorker.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace ExchangeRatesWorker.Logic.Helpers
{
    public static class DateHelper
    {
        private static readonly string formatDateTime = "yyyy-MM-dd";

        public static string ToFormattedString(this DateTime input)
        {
            return input.ToString(formatDateTime);
        }
        public static IEnumerable<string> TransformToFormattedDateStrings(this IEnumerable<DateTime> input)
        {
            return input.Select(d => d.ToFormattedString());
        }
            public static IEnumerable<DateTime> TransformToDates(this string input)
        {
            var datesOutput = new List<DateTime>();
            IFormatProvider culture = new CultureInfo("en-US", true);
            var invalidDates = new List<string>();

            input.Replace(" ", string.Empty).Split(',').ToList().ForEach(date =>
            {
                //DateTime dateVal = DateTime.ParseExact(date, "yyyy-MM-dd", culture);
                if (DateTime.TryParseExact(date, formatDateTime, culture, DateTimeStyles.None, out DateTime dateInput))
                {
                    datesOutput.Add(dateInput);
                }
                else
                {
                    invalidDates.Add(date);
                }
            });

            if (invalidDates.Any())
            {
                throw new InputFormatArgumentException(nameof(ExchangeRatesParams.Dates), $"{string.Join(",", invalidDates)}. Required date format: {formatDateTime}");
            }
            else if (!datesOutput.Any())
            {
                throw new InputMissingException($"Dates missing");
            }

            return datesOutput;
        }

        //public static IEnumerable<DateTime> TransformToDateTimeRange(this string input)
        //{
        //    var setOfDates = input.TransformToDates();

        //    var startDate = setOfDates.Min();
        //    var endDate = setOfDates.Max();

        //    while (startDate <= endDate)
        //    {
        //        yield return startDate;
        //        startDate = startDate.AddDays(1);
        //    }
        //}
    }
}
