using System;
using System.Collections.Generic;
using UnityEngine;

namespace Bootstrap.Currency
{
    public class CurrencyModel : IDisposable
    {
        public event Action<CurrencyType, ulong, ulong> OnCurrencyChanged;

        private readonly Dictionary<CurrencyType, CurrencyData> _currencies = new();

        private const string GOLD_CURRENCY = "GoldCurrency";
        private const string DIAMOND_CURRENCY = "DiamondCurrency";


        public CurrencyModel()
        {
            LoadData();
        }

        private void LoadData()
        {
            _currencies.Add(CurrencyType.Gold, new CurrencyData(GOLD_CURRENCY, 0));
            ParseData(CurrencyType.Gold);

            _currencies.Add(CurrencyType.Diamond, new CurrencyData(DIAMOND_CURRENCY, 0));
            ParseData(CurrencyType.Diamond);
        }

        public void AddCurrency(CurrencyType type, ulong value)
        {
            if (!_currencies.TryGetValue(type, out CurrencyData data)) return;

            if (data.Currency > ulong.MaxValue - value)
            {
                data.Currency = ulong.MaxValue;
            }
            else data.Currency += value;

            OnChange(type, data, value);
        }

        public void RemoveCurrency(CurrencyType type, ulong value)
        {
            if (!_currencies.TryGetValue(type, out CurrencyData data)) return;

            if (data.Currency < ulong.MinValue + value)
            {
                data.Currency = ulong.MinValue;
            }
            else data.Currency -= value;

            OnChange(type, data, value);
        }

        private void ParseData(CurrencyType type)
        {
            CurrencyData currencyData = _currencies[type];
            var value = PlayerPrefs.GetString(currencyData.Name);
            var currencyValue = ulong.TryParse(value, out var result) ? result : 0;
            currencyData.Currency = currencyValue;
        }

        private void OnChange(CurrencyType type, CurrencyData data, ulong value)
        {
            PlayerPrefs.SetString(data.Name, data.Currency.ToString());
            
            OnCurrencyChanged?.Invoke(type, data.Currency, value);
        }

        public void Dispose()
        {
            _currencies.Clear();
        }
    }
}