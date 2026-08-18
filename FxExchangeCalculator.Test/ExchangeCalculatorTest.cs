using Shouldly;

namespace FxExchangeCalculator.Test;

public class ExchangeCalculatorTest
{
    private readonly CurrencyExchangeService _exchangeService;
    public ExchangeCalculatorTest()
    {
        _exchangeService = new CurrencyExchangeService(new DkkExchangeRateProvider());
    }

    private decimal CalcExpectedAmount(decimal toCurrencyDkkRate, decimal fromCurrencyDkkRate, decimal amount) =>
        amount * toCurrencyDkkRate / fromCurrencyDkkRate;

    [Fact]
    public void DirectAndViaThirdCurrencyShouldBeSame()
    {
        // Arrange
        var amount = 100;

        // Act
        var euroAmount = _exchangeService.Exchange(Currency.DKK, Currency.EUR, amount);
        var dollarViaEuroAmount = _exchangeService.Exchange(Currency.EUR, Currency.USD, euroAmount);
        var dollarDirectAmount = _exchangeService.Exchange(Currency.DKK, Currency.USD, amount);

        // Assert
        dollarDirectAmount.ShouldBe(dollarViaEuroAmount);
    }

    [Theory]
    [InlineData(Currency.DKK)]
    [InlineData(Currency.USD)]
    [InlineData(Currency.JPY)]
    [InlineData(Currency.SEK)]
    public void Exchange_SameCurrency_ReturnsOriginalAmount(Currency currency)
    {
        // Arrange
        var exchangeAmount = 123;

        // Act
        var exchangedAmount = _exchangeService.Exchange(currency, currency, exchangeAmount);

        // Assert
        exchangedAmount.ShouldBe(exchangeAmount);
    }

    public static TheoryData<(Currency, Currency, decimal, decimal)> Exchange_ReturnsExpectedAmount_Data =>
    [
        (Currency.DKK, Currency.USD, DkkExchangeRateData.DKK, DkkExchangeRateData.USD),
        (Currency.USD, Currency.EUR, DkkExchangeRateData.USD, DkkExchangeRateData.EUR),
        (Currency.JPY, Currency.CHF, DkkExchangeRateData.JPY, DkkExchangeRateData.CHF),
        (Currency.DKK, Currency.SEK, DkkExchangeRateData.DKK, DkkExchangeRateData.SEK),
        (Currency.NOK, Currency.DKK, DkkExchangeRateData.NOK, DkkExchangeRateData.DKK)
    ];

    [Theory]
    [MemberData(nameof(Exchange_ReturnsExpectedAmount_Data))]
    public void Exchange_ReturnsExpectedAmount((Currency toCurrency, Currency fromCurrency, decimal toRate, decimal fromRate) testData)
    {
        // Arrange
        var exchangeAmount = 123;
        var rateProvider = new DkkExchangeRateProvider();
        var exchangeRateCalculator = new CurrencyExchangeService(rateProvider);

        // Act
        var exchangedAmount = exchangeRateCalculator.Exchange(testData.toCurrency, testData.fromCurrency, exchangeAmount);

        // Assert
        var expectedAmount = CalcExpectedAmount(testData.toRate, testData.fromRate, exchangeAmount);
        exchangedAmount.ShouldBe(expectedAmount);
    }
}
