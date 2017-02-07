using System;
using System.Linq;
using System.Text.RegularExpressions;
using CurrencyConverter.Bot.Models.Currency;
using CurrencyConverter.Bot.Models.Currency.Base;
using CurrencyConverter.Bot.Models.Response;
using HtmlAgilityPack;
using Newtonsoft.Json;
using Enum = CurrencyConverter.Bot.Models.Enumerations.Enum;

namespace CurrencyConverter.Bot.Integration
{
    public class CurrecnyConverterBloomberg : ICurrencyConverter
    {
        private static readonly NLog.ILogger Logger = NLog.LogManager.GetLogger("CurrencyConverter");

        public CurrencyConvertResponse FindRate(BaseCurrency crr)
        {
            var response = new CurrencyConvertResponse
            {
                IsSuccess = false,
                Message = "Unknows",
                Rate = 0m,
                Status = Enum.Status.Unknown
            };

            var line = crr is Dollar ? "USD/TRY" : crr is Euro ? "EUR/TRY" : "ERROR";


            var web = new HtmlWeb();
            var doc = web.Load("http://www.bloomberght.com/");

            var currencyRate = doc.DocumentNode.SelectNodes("//div[@class='line2']").FirstOrDefault(c => c.ParentNode.InnerHtml.Contains(line));

            if (currencyRate == null)
            {
                // log error
                Logger.Error($"CurrencyRate is null");

                return response;
            }

            var rateContent = Regex.Replace(currencyRate.InnerText.ToLower(), @"\s+", string.Empty);
            rateContent = rateContent.Replace(",", ".");
            var rate = 0m;

            #region Exception Handling

            try
            {
                rate = Convert.ToDecimal(rateContent);
                Console.WriteLine("The string as a decimal is {0}.", rate);
            }
            catch (OverflowException ex)
            {
                Logger.Error($"Error while calculation rate, OverflowException", ex);
                response.Status = Enum.Status.Error;
                response.Message = "The conversion from string to decimal overflowed.";
            }
            catch (FormatException ex)
            {
                Logger.Error($"Error while calculation rate, FormatException", ex);
                response.Status = Enum.Status.Error;
                response.Message = "The string is not formatted as a decimal.";
            }
            catch (ArgumentNullException ex)
            {
                Logger.Error($"Error while calculation rate, ArgumentNullException", ex);
                response.Status = Enum.Status.Error;
                response.Message = "The string is null.";
            }

            #endregion

            response.IsSuccess = true;
            response.Message = "Well done kereta";
            response.Rate = rate;
            response.Status = Enum.Status.Success;

            Logger.Debug($"Response : {JsonConvert.SerializeObject(response)}");

            return response;

        }

    }
}