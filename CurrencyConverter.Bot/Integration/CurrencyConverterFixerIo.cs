using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using CurrencyConverter.Bot.Models.Currency;
using CurrencyConverter.Bot.Models.Currency.Base;
using CurrencyConverter.Bot.Models.Response;
using Newtonsoft.Json;
using Enum = CurrencyConverter.Bot.Models.Enumerations.Enum;

namespace CurrencyConverter.Bot.Integration
{
    public class FixerModel
    {
        public string Base { get; set; }

        public DateTime Date { get; set; }

        public Rate Rates { get; set; }
    }

    public class Rate
    {
        public decimal Try { get; set; }

        public decimal Eur { get; set; }

        public decimal Usd { get; set; }
    }

    public class CurrencyConverterFixerIo : ICurrencyConverter
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

            var line = crr is Dollar ? "USD" : crr is Euro ? "EUR" : "ERROR";

            StringBuilder sb = new StringBuilder("http://api.fixer.io/latest?base=");
            sb.Append(line); // currency

            try
            {
                using (var client = new WebClient())
                {
                    var json = client.DownloadString(sb.ToString());
                    var rate = JsonConvert.DeserializeObject<FixerModel>(json);

                    response.IsSuccess = true;
                    response.Message = "Well done kereta";
                    response.Rate = rate.Rates.Try;
                    response.Status = Enum.Status.Success;
                }
            }
            catch (Exception ex)
            {
                Logger.Error($"Error while calculation rate. ex : {ex.StackTrace} ex.Message = {ex.Message}");
                response.Status = Enum.Status.Error;
                response.Message = "Error while calculation rate.";
            }


            Logger.Debug($"Response : {JsonConvert.SerializeObject(response)}");

            return response;
        }
    }
}