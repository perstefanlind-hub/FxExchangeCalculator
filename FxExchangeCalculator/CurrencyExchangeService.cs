namespace FxExchangeCalculator;

public class CurrencyExchangeService(IDkkExchangeRateProvider dkkExchangeRateProvider) : ICurrencyExchangeService
{
    public decimal Exchange(Currency fromCurrency, Currency toCurrency, decimal amountToBeExchanged)
    {
        var fromCurrencyDkkExchangeRate = dkkExchangeRateProvider.GetExchangeRate(fromCurrency);
        var toCurrencyDkkExchangeRate = dkkExchangeRateProvider.GetExchangeRate(toCurrency);
        return amountToBeExchanged * fromCurrencyDkkExchangeRate / toCurrencyDkkExchangeRate;
    }

}
