using FxExchangeCalculator.Exceptions;
using Shouldly;

namespace FxExchangeCalculator.Test;

public class InputParserTest
{
    public static TheoryData<(string, Currency, Currency, decimal)> ValidInputData =>
    [
        ("SEK/DKK 100", Currency.SEK, Currency.DKK, 100m),
        ("EUR/JPY 780,5", Currency.EUR, Currency.JPY, 780.5m),
        ("GBP/NOK 123,123", Currency.GBP, Currency.NOK, 123.123m),
    ];

    [Theory]
    [MemberData(nameof(ValidInputData))]
    public void ShouldParseValidInput((string input, Currency expectedFromCurrency, Currency expectedToCurrency, decimal expectedAmount) testData)
    {
        // Act
        var (fromCurrency, toCurrency, amount) = InputParser.Parse(testData.input);

        // Assert
        fromCurrency.ShouldBe(testData.expectedFromCurrency);
        toCurrency.ShouldBe(testData.expectedToCurrency);
        amount.ShouldBe(testData.expectedAmount);
    }

    [Theory]
    [InlineData("SEL/DKK 100")]
    [InlineData("EUR/XXX 780,0")]
    [InlineData("GPA/NOK 123,123")]
    public void ShouldThrowOnInvalidCurrency(string invalidCurrencyInput)
    {
        // Act / Assert
        Should.Throw<CurrencyNotSupportedException>(() => InputParser.Parse(invalidCurrencyInput));
    }

    [Theory]
    [InlineData("")]
    [InlineData("EUR-SEK 123")]
    [InlineData("DKK/NOK123")]
    [InlineData("DKK/SEK")]
    [InlineData("EUR/DKK 12.1")]
    public void ShouldThrowOnInvalidCommand(string invalidInput)
    {
        // Act / Assert
        Should.Throw<InvalidInputException>(() => InputParser.Parse(invalidInput));
    }
}
