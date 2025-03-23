using UnityEngine;
using Utils.UI.Buttons;

namespace MainMenu.UI.Popup
{
    public class MainMenuPopup : MonoBehaviour
    {
        [SerializeField] private BaseButton _closeButton;
        [SerializeField] private RectTransform _container;


        public void ActivatePopup()
        {
            _closeButton.onClick.AddListener(ClosePopup);
        }

        private void ClosePopup()
        {
            Destroy(gameObject);
        }

        public void AssignCamera(Camera canvasCamera)
        {
            GetComponent<Canvas>().worldCamera = canvasCamera;
        }

        public RectTransform GetContainer()
        {
            return _container;
        }
    }
}