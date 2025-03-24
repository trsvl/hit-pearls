using System.Threading;
using Bootstrap;
using Bootstrap.Audio;
using Bootstrap.UI;
using Cysharp.Threading.Tasks;
using Gameplay.Animations;
using Gameplay.Utils;
using UnityEngine;

namespace Gameplay.UI.Popup
{
    public class PopupController : IPauseGame, IResumeGame, ILoseGame, IFinishGame
    {
        private readonly GamePopup _gamePopupPrefab;
        private readonly Transform _canvases;
        private readonly PlayerData _playerData;
        private readonly GameplayStateObserver _gameplayStateObserver;
        private readonly Loader _loader;
        private readonly UIAnimation _uiAnimation;
        private readonly CameraController _cameraController;
        private readonly VolumePresenter _volumePresenter;
        private readonly CancellationToken _cancellationToken;
        private GamePopup _gamePopup;


        public PopupController(GamePopup gamePopupPrefab, Transform canvases, PlayerData playerData,
            GameplayStateObserver gameplayStateObserver, Loader loader, UIAnimation uiAnimation,
            CameraController cameraController, VolumePresenter volumePresenter, CancellationToken cancellationToken)
        {
            _gamePopupPrefab = gamePopupPrefab;
            _canvases = canvases;
            _playerData = playerData;
            _gameplayStateObserver = gameplayStateObserver;
            _loader = loader;
            _uiAnimation = uiAnimation;
            _cameraController = cameraController;
            _volumePresenter = volumePresenter;
            _cancellationToken = cancellationToken;
        }

        public void PauseGame()
        {
            CreatePopup();
            _gamePopup.PauseGame(RestartGameClick, ResumeGameCLick, MainMenuClick);
            _volumePresenter.CreateVolumeSliders(_gamePopup.GetContainer().transform);
        }

        public void ResumeGame()
        {
            _gamePopup.ResumeGame();
        }

        public void FinishGame()
        {
            CreatePopup();
            _gamePopup.FinishGame(_playerData.IsMaxLevel(), NextLevelClick, RestartGameClick, MainMenuClick);
        }

        public void LoseGame()
        {
            CreatePopup();
            _gamePopup.LoseGame(RestartGameClick, MainMenuClick);
        }

        private void CreatePopup()
        {
            _gamePopup = Object.Instantiate(_gamePopupPrefab, _canvases);
            _gamePopup.AssignCamera(_cameraController.GetUICamera());
            _uiAnimation.Move(_gamePopup.GetContainer(), 0.25f, _cancellationToken, initialOffset: (0f, -1000f))
                .Forget();
        }

        private void NextLevelClick()
        {
            _playerData.CurrentLevel += 1;
            _ = _loader.LoadScene(SceneName.Gameplay);
        }

        private void ResumeGameCLick()
        {
            _gameplayStateObserver.ResumeGame();
        }

        private void RestartGameClick()
        {
            _ = _loader.LoadScene(SceneName.Gameplay);
        }

        private void MainMenuClick()
        {
            _ = _loader.LoadScene(SceneName.MainMenu);
        }
    }
}