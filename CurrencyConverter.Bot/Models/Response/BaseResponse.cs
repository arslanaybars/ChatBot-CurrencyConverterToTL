using Enum = CurrencyConverter.Bot.Models.Enumerations.Enum;

namespace CurrencyConverter.Bot.Models.Response
{
    public class BaseResponse
    {
        public string Message { get; set; }

        public bool IsSuccess { get; set; }

        public decimal Rate { get; set; }

        public Enum.Status Status { get; set; }
    }
}