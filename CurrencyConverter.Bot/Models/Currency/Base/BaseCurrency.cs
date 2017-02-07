using System.Collections.Generic;

namespace CurrencyConverter.Bot.Models.Currency.Base
{
    public abstract class BaseCurrency
    {
        public abstract string Name { get; }

        public decimal Rate { get; set; }

        public abstract List<string> SynonymousCurrency();

        public abstract decimal CalculateRatee();
    }
}