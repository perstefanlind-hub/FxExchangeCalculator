namespace FxExchangeCalculator;

public class DkkExchangeRateProvider : IDkkExchangeRateProvider
{
    // Returns how many DKK 100 of given currency can buy.
    public decimal GetExchangeRate(Currency currency) =>
        currency switch
        {
            Currency.DKK => DkkExchangeRateData.DKK,
            Currency.EUR => DkkExchangeRateData.EUR,
            Currency.USD => DkkExchangeRateData.USD,
            Currency.GBP => DkkExchangeRateData.GBP,
            Currency.SEK => DkkExchangeRateData.SEK,
            Currency.NOK => DkkExchangeRateData.NOK,
            Currency.CHF => DkkExchangeRateData.CHF,
            Currency.JPY => DkkExchangeRateData.JPY,
            _ => throw new NotImplementedException($"Exchange rate provider cannot provide rate for {currency}")
        };
}
