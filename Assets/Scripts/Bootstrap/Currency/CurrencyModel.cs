using System;
using System.Collections.Generic;
using Bootstrap.Player;
using UnityEngine;

namespace Bootstrap.Currency
{
    public class CurrencyModel : IDisposable
    {
        public event Action<CurrencyType, ulong, ulong> OnCurrencyChanged;

        private readonly SaveManager _saveManager;

        private readonly Dictionary<CurrencyType, ulong> _currencies = new();

        private const string CURRENCY_KEY = "Currency";


        public CurrencyModel(SaveManager saveManager)
        {
            _saveManager = saveManager;

            var currencyData = _saveManager.Load<CurrencyData>(CURRENCY_KEY);

            _currencies.Add(CurrencyType.Gold, currencyData.GoldCurrency);
            _currencies.Add(CurrencyType.Diamond, currencyData.DiamondCurrency);
        }

        public void AddCurrency(CurrencyType type, ulong value)
        {
            if (!_currencies.TryGetValue(type, out ulong currency)) return;

            if (currency > ulong.MaxValue - value)
            {
                currency = ulong.MaxValue;
            }
            else currency += value;

            _currencies[type] = currency;

            OnChange(type, currency, value);
        }

        public void RemoveCurrency(CurrencyType type, ulong value)
        {
            if (!_currencies.TryGetValue(type, out ulong currency)) return;

            if (currency < ulong.MinValue + value)
            {
                currency = ulong.MinValue;
            }
            else currency -= value;

            _currencies[type] = currency;

            OnChange(type, currency, value);
        }

        private void OnChange(CurrencyType type, ulong currency, ulong value)
        {
            _saveManager.Save(CURRENCY_KEY, SaveCurrencies());

            OnCurrencyChanged?.Invoke(type, currency, value);
        }

        private CurrencyData SaveCurrencies()
        {
            var data = new CurrencyData
            {
                GoldCurrency = _currencies[CurrencyType.Gold],
                DiamondCurrency = _currencies[CurrencyType.Diamond]
            };

            return data;
        }

        public void Dispose()
        {
            _currencies.Clear();
        }
    }
}