namespace FxExchangeCalculator;

public interface ICurrencyExchangeService
{
    public decimal Exchange(Currency fromCurrency, Currency toCurrency, decimal amountToBeExchanged);
}
