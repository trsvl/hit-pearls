using UnityEngine;
using UnityEngine.UI;
using Utils.UI;

namespace Bootstrap.Audio
{
    public class VolumeVIew
    {
        private readonly SliderObject _sliderPrefab;


        public VolumeVIew(SliderObject sliderPrefab)
        {
            _sliderPrefab = sliderPrefab;
        }

        public Slider CreateSFXSlider(Transform parent)
        {
            SliderObject sfxObject = Object.Instantiate(_sliderPrefab, parent);
            sfxObject.LabelText.SetText("SFX");
            return sfxObject.Slider;
        }

        public Slider CreateMusicSlider(Transform parent)
        {
            SliderObject musicSlider = Object.Instantiate(_sliderPrefab, parent);
            musicSlider.LabelText.SetText("Music");
            return musicSlider.Slider;
        }
    }
}