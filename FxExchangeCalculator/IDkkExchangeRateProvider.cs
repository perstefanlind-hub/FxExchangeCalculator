namespace FxExchangeCalculator;

public interface IDkkExchangeRateProvider
{
    decimal GetExchangeRate(Currency currency);
}
