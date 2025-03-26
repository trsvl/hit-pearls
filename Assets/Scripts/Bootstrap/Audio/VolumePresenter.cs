using UnityEngine;
using UnityEngine.UI;
using VContainer.Unity;

namespace Bootstrap.Audio
{
    public class VolumePresenter : IInitializable
    {
        private readonly VolumeModel _volumeModel;
        private readonly VolumeVIew _volumeView;


        public VolumePresenter(VolumeModel volumeModel, VolumeVIew volumeView)
        {
            _volumeModel = volumeModel;
            _volumeView = volumeView;
        }

        public void CreateVolumeSliders(Transform parent)
        {
            Slider sfxSlider = _volumeView.CreateSFXSlider(parent);
            Slider musicSlider = _volumeView.CreateMusicSlider(parent);

            _volumeModel.CreateVolumeSliders(sfxSlider, musicSlider);
        }

        public void Initialize()
        {
            _volumeModel.Initialize();
        }
    }
}