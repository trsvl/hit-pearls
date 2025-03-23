using System.Threading;
using Bootstrap;
using Bootstrap.Audio;
using Bootstrap.UI;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace MainMenu.UI.Popup
{
    public class PopupController
    {
        private readonly MainMenuPopup _gamePopupPrefab;
        private readonly Transform _canvases;
        private readonly UIAnimation _uiAnimation;
        private readonly CameraController _cameraController;
        private readonly VolumePresenter _volumePresenter;
        private readonly CancellationToken _cancellationToken;


        public PopupController(MainMenuPopup gamePopupPrefab, Transform canvases, UIAnimation uiAnimation,
            CameraController cameraController, VolumePresenter volumePresenter, CancellationToken cancellationToken)
        {
            _gamePopupPrefab = gamePopupPrefab;
            _canvases = canvases;
            _uiAnimation = uiAnimation;
            _cameraController = cameraController;
            _volumePresenter = volumePresenter;
            _cancellationToken = cancellationToken;
        }

        public void ActivatePopup()
        {
            var popup = CreatePopup();
            popup.ActivatePopup();
            _volumePresenter.CreateVolumeSliders(popup.GetContainer().transform);
        }

        private MainMenuPopup CreatePopup()
        {
            MainMenuPopup popup = Object.Instantiate(_gamePopupPrefab, _canvases);
            popup.AssignCamera(_cameraController.GetUICamera());
            _uiAnimation.Move(popup.GetContainer(), 0.25f, _cancellationToken, initialOffset: (0f, -1000f))
                .Forget();

            return popup;
        }
    }
}