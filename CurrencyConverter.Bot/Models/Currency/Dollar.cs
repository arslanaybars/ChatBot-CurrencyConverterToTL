using System.Collections.Generic;
using CurrencyConverter.Bot.Models.Currency.Base;

namespace CurrencyConverter.Bot.Models.Currency
{
    public class Dollar : BaseCurrency
    {
        public override string Name => "Usd";

        public override List<string> SynonymousCurrency()
        {
            return new List<string> { "💵", "dollar", "usd", "dolar", "$"}; 
        }

        public override decimal CalculateRatee()
        {
            return 5m;
        }
    }

}