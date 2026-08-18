// See https://aka.ms/new-console-template for more information
using FxExchangeCalculator;
using FxExchangeCalculator.Exceptions;
using System.Text.RegularExpressions;

var rateProvider = new DkkExchangeRateProvider();
var exchangeService = new CurrencyExchangeService(rateProvider);

Console.WriteLine("FX Exchange calculator");
Console.WriteLine("Type 'help' for help, 'list' to list supported currencies and 'exit' to quit");


while (true)
{
    Console.Write("> ");
    var input = Console.ReadLine()?.Trim();
    if (string.IsNullOrWhiteSpace(input))
        continue;

    if (input.Equals("exit", StringComparison.OrdinalIgnoreCase))
        break;

    if (input.Equals("help", StringComparison.OrdinalIgnoreCase))
    {
        PrintHelp();
        continue;
    }

    if (input.Equals("list", StringComparison.OrdinalIgnoreCase))
    {
        PrintSupportedCurrencies();
        continue;
    }
    try
    {
        (Currency fromCurrency, Currency toCurrency, decimal amount) = InputParser.Parse(input);
        var exchangedAmount = exchangeService.Exchange(fromCurrency, toCurrency, amount);
        Console.WriteLine(exchangedAmount.ToString("F2"));
    }
    catch (Exception ex) when (ex is CurrencyNotSupportedException or InvalidAmountException or InvalidInputException)
    {
        Console.WriteLine(ex.Message);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Something unexpected happened: {ex.Message}"); 
    }
}

static void PrintHelp()
{
    Console.WriteLine("This is a FX Calculator, where an amount in one currency can be exchanged to an amount in a different currency.");
    Console.WriteLine("Usage: <from currency>/<to currency> <amount>.");
    Console.WriteLine("Example: DKK/SEK 100");
}

static void PrintSupportedCurrencies()
{
    Console.WriteLine($"Supported currencies are: {string.Join(", ", Enum.GetValues<Currency>())}");
}

public static partial class RegexConstants
{

    [GeneratedRegex(@"^(?<fromCurrency>[A-Z]{3})\/(?<toCurrency>[A-Z]{3}) (?<amount>\d+(,\d+)?)$")]
    public static partial Regex ExchangeCommandRegex();
}