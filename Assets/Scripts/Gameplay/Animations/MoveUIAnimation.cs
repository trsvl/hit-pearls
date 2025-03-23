using System.Threading;
using Bootstrap.UI;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using MainMenu.UI.Header;
using UnityEngine;

namespace Gameplay.Animations
{
    public class MoveUIAnimation
    {
        private readonly RectTransform _header;
        private readonly RectTransform _pauseButton;
        private readonly MainMenuHeaderManager _mainMenuHeaderManager;
        private readonly UIAnimation _uiAnimation;
        private readonly CancellationToken _cancellationToken;


        public MoveUIAnimation(RectTransform header, RectTransform pauseButton,
            MainMenuHeaderManager mainMenuHeaderManager, UIAnimation uiAnimation, CancellationToken cancellationToken)
        {
            _header = header;
            _pauseButton = pauseButton;
            _mainMenuHeaderManager = mainMenuHeaderManager;
            _uiAnimation = uiAnimation;
            _cancellationToken = cancellationToken;

            _header.gameObject.SetActive(false);
            _pauseButton.gameObject.SetActive(false);
        }

        public void MoveOnStart()
        {
            const float duration = 0.15f;

            _uiAnimation.Move(_header, duration, _cancellationToken, initialOffset: (0f, 500f)).Forget();
            _uiAnimation.Move(_pauseButton, duration, _cancellationToken, initialOffset: (500f, 0f)).Forget();
        }

        public async UniTask ChangeHeader(float duration)
        {
            Vector2 targetPosition = _header.anchoredPosition;

            await _uiAnimation.Move(_header, duration, _cancellationToken, targetOffset: (0f, 500f));
            Object.Destroy(_header.gameObject);

            RectTransform mainMenuHeader = _mainMenuHeaderManager.CreateHeader();
            await _uiAnimation.Move(mainMenuHeader, duration, _cancellationToken, initialOffset: (0f, 500f),
                targetPosition: targetPosition);
        }
    }
}