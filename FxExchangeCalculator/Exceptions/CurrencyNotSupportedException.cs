namespace FxExchangeCalculator.Exceptions;

public class CurrencyNotSupportedException(string currency)
    : Exception($"'{currency}' is not a supported currency. Type 'list' to list supported currencies.")
{
}
