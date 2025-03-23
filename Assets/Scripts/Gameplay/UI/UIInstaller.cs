﻿using Gameplay.UI.Buttons;
using Gameplay.UI.Header;
using Gameplay.UI.Popup;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils.UI.Buttons;
using VContainer;
using VContainer.Unity;

namespace Gameplay.UI
{
    public class UIInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField] private TextMeshProUGUI _pearlsText;
        [SerializeField] private TextMeshProUGUI _shotsText;

        [Space] [SerializeField] private BaseButton _pauseButton;
        [SerializeField] private Button _respawnBallButton;

        [Space] [SerializeField] private GamePopup _gamePopupPrefab;
        [SerializeField] private Transform _canvases;


        public void Install(IContainerBuilder builder)
        {
            builder.Register<PearlsData>(Lifetime.Scoped)
                .WithParameter("pearlsText", _pearlsText);

            builder.Register<ShotsData>(Lifetime.Scoped)
                .WithParameter("shotsText", _shotsText);

            builder.Register<PauseButton>(Lifetime.Scoped)
                .WithParameter(_pauseButton);

            builder.Register<RespawnBallButton>(Lifetime.Scoped)
                .WithParameter(_respawnBallButton);

            builder.Register<PopupController>(Lifetime.Scoped)
                .WithParameter("gamePopupPrefab", _gamePopupPrefab)
                .WithParameter(_canvases);
        }
    }
}