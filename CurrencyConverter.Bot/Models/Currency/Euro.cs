using System.Collections.Generic;
using CurrencyConverter.Bot.Models.Currency.Base;

namespace CurrencyConverter.Bot.Models.Currency
{
    public class Euro : BaseCurrency
    {
        public override string Name => "Eur";

        public override List<string> SynonymousCurrency()
        {
            return new List<string> { "💶", "euro", "eur", "€" };
        }

        public override decimal CalculateRatee()
        {
            return 6m;
        }
    }
}