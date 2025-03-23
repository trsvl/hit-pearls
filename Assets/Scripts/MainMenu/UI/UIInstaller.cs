using MainMenu.UI.Buttons;
using MainMenu.UI.Popup;
using UnityEngine;
using Utils.UI.Buttons;
using VContainer;
using VContainer.Unity;

namespace MainMenu.UI
{
    public class UIInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField] private BaseButton _pauseButton;
        [SerializeField] private MainMenuPopup _gamePopupPrefab;
        [SerializeField] private Transform _canvases;


        public void Install(IContainerBuilder builder)
        {
            builder.Register<PauseButton>(Lifetime.Scoped)
                .WithParameter(_pauseButton);

            builder.Register<PopupController>(Lifetime.Scoped)
                .WithParameter(_gamePopupPrefab)
                .WithParameter(_canvases);
        }
    }
}