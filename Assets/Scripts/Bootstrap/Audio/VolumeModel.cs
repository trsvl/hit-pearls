using System.Threading;
using Bootstrap.Player;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Bootstrap.Audio
{
    public class VolumeModel
    {
        private readonly AudioMixer _mixer;
        private readonly SaveManager _saveManager;
        private CancellationTokenSource _saveDataCts;

        private Slider _sfxSlider;
        private Slider _musicSlider;

        private const string VOLUME_KEY = "Volume";
        private const string Mixer_SFX = "SFX";
        private const string Mixer_Music = "Music";

        private VolumeData _volumeData;


        public VolumeModel(AudioMixer mixer, SaveManager saveManager)
        {
            _mixer = mixer;
            _saveManager = saveManager;
        }

        public void Initialize()
        {
            _volumeData = _saveManager.Load<VolumeData>(VOLUME_KEY);

            _mixer.SetFloat(Mixer_SFX, SetVolume(_volumeData.SFXVolume));
            _mixer.SetFloat(Mixer_Music, SetVolume(_volumeData.MusicVolume));
        }

        public void CreateVolumeSliders(Slider sfxSlider, Slider musicSlider)
        {
            _sfxSlider = sfxSlider;
            _musicSlider = musicSlider;

            _sfxSlider.value = _volumeData.SFXVolume;
            _musicSlider.value = _volumeData.MusicVolume;

            _sfxSlider.onValueChanged.AddListener(SetSFXVolume);
            _musicSlider.onValueChanged.AddListener(SetMusicVolume);
        }

        private void SetSFXVolume(float value)
        {
            _mixer.SetFloat(Mixer_SFX, SetVolume(value));
            SaveData().Forget();
        }


        private void SetMusicVolume(float value)
        {
            _mixer.SetFloat(Mixer_Music, SetVolume(value));
            SaveData().Forget();
        }

        private float SetVolume(float value)
        {
            float volume = (value > 0) ? Mathf.Log10(value) * 20 : -80f;
            return volume;
        }

        private async UniTask SaveData()
        {
            _saveDataCts?.Cancel();
            _saveDataCts = new CancellationTokenSource();

            await UniTask.WaitForSeconds(0.5f, ignoreTimeScale: true, cancellationToken: _saveDataCts.Token);

            PlayerPrefs.SetFloat(Mixer_SFX, _sfxSlider.value);
            PlayerPrefs.SetFloat(Mixer_Music, _musicSlider.value);

            _volumeData.SFXVolume = _sfxSlider.value;
            _volumeData.MusicVolume = _musicSlider.value;

            _saveManager.Save(VOLUME_KEY, _volumeData);
        }
    }
}