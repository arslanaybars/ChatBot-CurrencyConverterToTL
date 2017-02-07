using System.Collections.Generic;
using CurrencyConverter.Bot.Models.Currency;
using CurrencyConverter.Bot.Models.Currency.Base;

namespace CurrencyConverter.Bot.Disctionary
{
    public class StringToCurrency
    {
        public static Dictionary<string, BaseCurrency> Dictionary => new Dictionary<string, BaseCurrency>
        {
            {"dollar", new Dollar()},
            {"dolar", new Dollar()},
            {"usd", new Dollar()},
            {"$", new Dollar()},
            {"💵", new Dollar()},
            {"euro", new Euro()},
            {"avro", new Euro()},
            {"eur", new Euro()},
            {"€", new Euro()},
            {"💶", new Euro()},
        };
    }
}