using System;
using MainMenu.UI.Popup;
using Utils.UI.Buttons;

namespace MainMenu.UI.Buttons
{
    public class PauseButton : IDisposable, IMainMenuStart
    {
        private readonly BaseButton _button;
        private readonly PopupController _popupController;


        public PauseButton(BaseButton button, PopupController popupController)
        {
            _button = button;
            _popupController = popupController;
            _button.interactable = false;
        }

        public void OnMainMenuStart()
        {
            _button.Init(_popupController.ActivatePopup);

            _button.interactable = true;
        }

        public void Dispose()
        {
            _button.onClick.RemoveAllListeners();
        }
    }
}