namespace DCXAir.Domain.Interfaces
{
    public interface ICurrencyConverter
    {
        decimal Convert(decimal amount, string fromCurrency, string toCurrency);
    }
}