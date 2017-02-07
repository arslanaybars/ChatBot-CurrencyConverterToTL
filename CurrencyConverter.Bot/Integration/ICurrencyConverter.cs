using CurrencyConverter.Bot.Models.Currency.Base;
using CurrencyConverter.Bot.Models.Response;

namespace CurrencyConverter.Bot.Integration
{
    public interface ICurrencyConverter
    {
        CurrencyConvertResponse FindRate(BaseCurrency crr);
    }
}