using System.Linq;
using CurrencyConverter.Bot.Disctionary;
using CurrencyConverter.Bot.Integration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Enum = CurrencyConverter.Bot.Models.Enumerations.Enum;

namespace CurrencyConverter.Test
{
    [TestClass]
    public class IntegrationTest
    {
        [TestMethod]
        public void CurrecnyConverterBloomberg_CurrencyToTRY_True()
        {
            //Arrange
            var currency = StringToCurrency.Dictionary.FirstOrDefault(x => "dollar".ToLower().Contains(x.Key)).Value;

            //Act
            var result = new CurrecnyConverterBloomberg().FindRate(currency);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(Enum.Status.Success, result.Status);
            Assert.AreEqual(true, result.IsSuccess);
        }
    }
}
