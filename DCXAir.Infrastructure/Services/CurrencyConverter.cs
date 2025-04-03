using DCXAir.Domain.Interfaces;


namespace DCXAir.Infrastructure.Services
{
    public class CurrencyConverter : ICurrencyConverter
    {
        // Tasas de cambio simplificadas para el ejemplo
        private readonly Dictionary<string, decimal> _exchangeRates;

        public CurrencyConverter()
        {
            // Inicializar el diccionario en el constructor
            _exchangeRates = new Dictionary<string, decimal>(StringComparer.OrdinalIgnoreCase)
            {
                { "USD", 1.0m },
                { "EUR", 0.85m },
                { "GBP", 0.75m },
                { "COP", 3454.75m },
                
            };
        }
public decimal Convert(decimal amount, string fromCurrency, string toCurrency)
{
    Console.WriteLine($"Convirtiendo {amount} de {fromCurrency} a {toCurrency}");
    
    // Validar parámetros
    if (string.IsNullOrEmpty(fromCurrency) || string.IsNullOrEmpty(toCurrency))
    {
        Console.WriteLine("Advertencia: Moneda nula o vacía, devolviendo monto original");
        return amount;
    }

    // Si las monedas son iguales, no hay conversión necesaria
    if (fromCurrency.Equals(toCurrency, StringComparison.OrdinalIgnoreCase))
    {
        Console.WriteLine("Monedas iguales, no se requiere conversión");
        return amount;
    }

    // Verificar si las monedas existen en el diccionario
    if (!_exchangeRates.ContainsKey(fromCurrency.ToUpper()))
    {
        Console.WriteLine($"Advertencia: Moneda de origen '{fromCurrency}' no encontrada");
        return amount;
    }
    
    if (!_exchangeRates.ContainsKey(toCurrency.ToUpper()))
    {
        Console.WriteLine($"Advertencia: Moneda de destino '{toCurrency}' no encontrada");
        return amount;
    }

    // Convertir a USD primero (moneda base)
    var amountInUsd = fromCurrency.Equals("USD", StringComparison.OrdinalIgnoreCase)
        ? amount
        : amount / _exchangeRates[fromCurrency.ToUpper()];
    
    Console.WriteLine($"Monto en USD: {amountInUsd}");

    // Luego convertir de USD a la moneda destino
    var result = toCurrency.Equals("USD", StringComparison.OrdinalIgnoreCase)
        ? amountInUsd
        : amountInUsd * _exchangeRates[toCurrency.ToUpper()];
    
    Console.WriteLine($"Resultado final: {result} {toCurrency}");
    
    return result;
}
    }
}