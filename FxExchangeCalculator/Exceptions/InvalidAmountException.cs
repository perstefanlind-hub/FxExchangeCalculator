namespace FxExchangeCalculator.Exceptions;

internal class InvalidAmountException(string amountInput)
    : Exception($"'{amountInput}' is not a valid decimal number.")
{
}
