namespace Bootstrap.Currency
{
    public class CurrencyData
    {
        public readonly string Name;
        public ulong Currency;

        public CurrencyData(string name, ulong currency)
        {
            Name = name;
            Currency = currency;
        }
    }
}