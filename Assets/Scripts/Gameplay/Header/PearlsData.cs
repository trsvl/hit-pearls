﻿using System.Text;
using TMPro;
using UnityEngine;
using Utils.EventBusSystem;

namespace Gameplay.Header
{
    public class PearlsData : IDestroySphere
    {
        private readonly TextMeshProUGUI _pearlsText;
        private readonly StringBuilder textBuilder;
        private int _currentNumber;


        public PearlsData(EventBus eventBus, TextMeshProUGUI pearlsText)
        {
            eventBus.Subscribe(this);

            _pearlsText = pearlsText;
            textBuilder = new StringBuilder();
        }

        private void UpdateText()
        {
            textBuilder.Clear();
            textBuilder.Append($"{_currentNumber}");
            _pearlsText.SetText(textBuilder);
        }

        public void OnDestroySphere(GameObject sphere)
        {
            _currentNumber += 1;
            UpdateText();
        }
    }
}