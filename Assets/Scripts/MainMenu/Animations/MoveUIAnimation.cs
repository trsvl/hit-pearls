using System.Threading;
using Bootstrap.UI;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace MainMenu.Animations
{
    public class MoveUIAnimation
    {
        private readonly RectTransform _header;
        private readonly RectTransform _pauseButton;
        private readonly RectTransform _footer;
        private readonly UIAnimation _uiAnimation;
        private readonly CancellationToken _cancellationToken;


        public MoveUIAnimation(RectTransform header, RectTransform pauseButton, RectTransform footer,
            UIAnimation uiAnimation, CancellationToken cancellationToken)
        {
            _header = header;
            _pauseButton = pauseButton;
            _footer = footer;
            _uiAnimation = uiAnimation;
            _cancellationToken = cancellationToken;

            _header.gameObject.SetActive(false);
            _pauseButton.gameObject.SetActive(false);
        }

        public async UniTask MoveOnStart()
        {
            const float duration = 0.5f;

            await UniTask.WhenAll(
                _uiAnimation.Move(_header, duration * 2, _cancellationToken, initialOffset: (0f, 500f)),
                _uiAnimation.Move(_pauseButton, duration * 2, _cancellationToken, initialOffset: (500f, 0f)),
                _uiAnimation.Move(_footer, duration, _cancellationToken, initialOffset: (0f, -1000f))
            );
        }
    }
}