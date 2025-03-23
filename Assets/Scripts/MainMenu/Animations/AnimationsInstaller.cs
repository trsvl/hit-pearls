using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace MainMenu.Animations
{
    public class AnimationsInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField] private RectTransform _header;
        [SerializeField] private RectTransform _pauseButton;
        [SerializeField] private RectTransform _footer;


        public void Install(IContainerBuilder builder)
        {
            builder.Register<MoveUIAnimation>(Lifetime.Scoped)
                .WithParameter("header", _header)
                .WithParameter("pauseButton", _pauseButton)
                .WithParameter("footer", _footer);
        }
    }
}