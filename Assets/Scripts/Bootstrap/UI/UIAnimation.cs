using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Bootstrap.UI
{
    public class UIAnimation
    {
        public async UniTask Move(RectTransform uiElement, float duration, CancellationToken cancellationToken,
            Vector2 initialPosition = default, (float, float) initialOffset = default, Vector2 targetPosition = default,
            (float, float) targetOffset = default)
        {
            targetPosition = targetPosition == default
                ? uiElement.anchoredPosition + new Vector2(targetOffset.Item1, targetOffset.Item2)
                : targetPosition + new Vector2(targetOffset.Item1, targetOffset.Item2);

            uiElement.anchoredPosition = initialPosition == default
                ? uiElement.anchoredPosition + new Vector2(initialOffset.Item1, initialOffset.Item2)
                : initialPosition + new Vector2(initialOffset.Item1, initialOffset.Item2);
            uiElement.gameObject.SetActive(true);

            await uiElement.DOAnchorPos(targetPosition, duration).SetEase(Ease.Linear).SetUpdate(true)
                .ToUniTask(cancellationToken: cancellationToken);
        }
    }
}