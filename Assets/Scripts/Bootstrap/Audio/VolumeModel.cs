using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Bootstrap.Audio
{
    public class VolumeModel
    {
        private readonly AudioMixer _mixer;
        private CancellationTokenSource _saveDataCts;

        private Slider _sfxSlider;
        private Slider _musicSlider;

        private const string MIXER_SFX = "SFX";
        private const string MIXER_MUSIC = "Music";


        public VolumeModel(AudioMixer mixer)
        {
            _mixer = mixer;
        }

        public void Initialize()
        {
            float sfxValue = PlayerPrefs.GetFloat(MIXER_SFX, 1f);
            float musicValue = PlayerPrefs.GetFloat(MIXER_MUSIC, 1f);

            _mixer.SetFloat(MIXER_SFX, SetVolume(sfxValue));
            _mixer.SetFloat(MIXER_MUSIC, SetVolume(musicValue));
        }

        public void CreateVolumeSliders(Slider sfxSlider, Slider musicSlider)
        {
            _sfxSlider = sfxSlider;
            _musicSlider = musicSlider;
            _sfxSlider.value = PlayerPrefs.GetFloat(MIXER_SFX, 1f);
            _musicSlider.value = PlayerPrefs.GetFloat(MIXER_MUSIC, 1f);

            _sfxSlider.onValueChanged.AddListener(SetSFXVolume);
            _musicSlider.onValueChanged.AddListener(SetMusicVolume);
        }

        private void SetSFXVolume(float value)
        {
            _mixer.SetFloat(MIXER_SFX, SetVolume(value));
            SaveData().Forget();
        }


        private void SetMusicVolume(float value)
        {
            _mixer.SetFloat(MIXER_MUSIC, SetVolume(value));
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

            PlayerPrefs.SetFloat(MIXER_SFX, _sfxSlider.value);
            PlayerPrefs.SetFloat(MIXER_MUSIC, _musicSlider.value);
        }
    }
}