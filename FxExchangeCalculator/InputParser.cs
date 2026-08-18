using FxExchangeCalculator.Exceptions;

namespace FxExchangeCalculator;

public class InputParser
{
    public static (Currency toCurrency, Currency fromCurrency, decimal amount) Parse(string input)
    {
        var match = RegexConstants.ExchangeCommandRegex().Match(input);
        if (match.Success)
        {
            var fromCurrencyInput = match.Groups["fromCurrency"].Value;
            var toCurrencyInput = match.Groups["toCurrency"].Value;
            var amountInput = match.Groups["amount"].Value;
            if (!TryParseCurrency(fromCurrencyInput, out var fromCurrency))
                throw new CurrencyNotSupportedException(fromCurrencyInput);
            if (!TryParseCurrency(toCurrencyInput, out var toCurrency))
                throw new CurrencyNotSupportedException(fromCurrencyInput);
            if (!decimal.TryParse(amountInput, out var amount))
                throw new InvalidAmountException(amountInput);

            return (fromCurrency, toCurrency, amount);
        }

        throw new InvalidInputException(input);
    }

    static bool TryParseCurrency(string currencyInput, out Currency toCurrency)
    {
        return Enum.TryParse(currencyInput, out toCurrency);
    }
}
