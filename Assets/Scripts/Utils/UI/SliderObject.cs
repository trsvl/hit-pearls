using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Utils.UI
{
    public class SliderObject : MonoBehaviour
    {
        [SerializeField] private Slider slider;
        [SerializeField] private TextMeshProUGUI labelText;

        public Slider Slider => slider;
        public TextMeshProUGUI LabelText => labelText;
    }
}