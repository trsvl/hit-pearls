using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Utils.Scene.AudioSystem;
using VContainer;

namespace Bootstrap.Audio
{
    public class AudioController : MonoBehaviour
    {
        public static AudioController Instance;

        private readonly Dictionary<AudioAction, List<Utils.Scene.AudioSystem.Audio>> audioClips = new();
        private CancellationToken _cancellationToken;
        private CancellationTokenSource linkedCts;


        [Inject]
        public AudioController()
        {
            Instance = this;
        }

        public void AssignNewToken(CancellationToken cancellationToken)
        {
            _cancellationToken = cancellationToken;
        }

        [Inject]
        public void SubscribeAudio(List<Audios> audios)
        {
            foreach (Audios arr in audios)
            {
                foreach (Utils.Scene.AudioSystem.Audio a in arr.audios)
                {
                    AudioSource audioSource = gameObject.AddComponent<AudioSource>();
                    audioSource.clip = a.clip;
                    audioSource.playOnAwake = a.playOnAwake;
                    audioSource.outputAudioMixerGroup = a.audioMixerGroup;
                    audioSource.volume = a.volume;
                    audioSource.pitch = a.pitch;
                    audioSource.loop = a.loop;
                    audioSource.time = a.startTime;

                    a.source = audioSource;

                    if (!audioClips.ContainsKey(arr.action))
                    {
                        audioClips[arr.action] = new List<Utils.Scene.AudioSystem.Audio>();
                    }

                    audioClips[arr.action].Add(a);
                }
            }
        }

        public void UnsubscribeAudio(List<Audios> audios)
        {
            foreach (Audios arr in audios)
            {
                foreach (Utils.Scene.AudioSystem.Audio a in arr.audios)
                {
                    if (audioClips.ContainsKey(arr.action) && audioClips[arr.action].Contains(a))
                    {
                        Destroy(a.source);
                        audioClips[arr.action].Remove(a);
                    }
                }
            }
        }

        public void Play(AudioAction audioAction)
        {
            audioClips[audioAction].ForEach(a =>
            {
                a.source.time = a.startTime;
                PlayAudio(a).Forget();
            });
        }

        public void PauseGame()
        {
            foreach (List<Utils.Scene.AudioSystem.Audio> audioList in audioClips.Values)
            {
                foreach (Utils.Scene.AudioSystem.Audio a in audioList)
                {
                    if (!a.source.isPlaying) continue;

                    PauseFade(a);
                }
            }
        }

        public void ResumeGame()
        {
            foreach (List<Utils.Scene.AudioSystem.Audio> audioList in audioClips.Values)
            {
                foreach (Utils.Scene.AudioSystem.Audio a in audioList)
                {
                    ResumeFade(a);
                }
            }
        }

        private async UniTask PlayAudio(Utils.Scene.AudioSystem.Audio a)
        {
            int delayDuration = (int)(a.delay * 1000);
            await UniTask.Delay(delayDuration, cancellationToken: _cancellationToken);
            a.source.Play();
            if (a.fadeInDuration != 0f) FadeIn(a);
            if (a.fadeOutDuration != 0f) FadeOut(a).Forget();
        }

        private void FadeIn(Utils.Scene.AudioSystem.Audio a)
        {
            FadeVolume(a.source, 0f, a.volume, a.fadeInDuration).Forget();
        }

        private async UniTask FadeOut(Utils.Scene.AudioSystem.Audio a)
        {
            float delayDuration = a.clip.length - a.fadeOutDuration - a.source.time;
            await UniTask.WaitForSeconds(delayDuration, cancellationToken: _cancellationToken);
            await FadeVolume(a.source, a.volume, 0f, a.fadeOutDuration);
        }

        private async UniTask FadeVolume(AudioSource audioSource, float startValue, float endValue, float fadeDuration,
            bool isLinkedToken = false, Action action = null)
        {
            var token = isLinkedToken ? linkedCts.Token : _cancellationToken;

            await DOTween.To(() => startValue, x => audioSource.volume = x, endValue, fadeDuration)
                .SetEase(Ease.InQuad).SetUpdate(true).ToUniTask(cancellationToken: token);
            action?.Invoke();
        }

        private void PauseFade(Utils.Scene.AudioSystem.Audio a)
        {
            linkedCts = new CancellationTokenSource();
            linkedCts = CancellationTokenSource.CreateLinkedTokenSource(linkedCts.Token, _cancellationToken);
            FadeVolume(a.source, a.volume, 0f, 0.2f, true, Action).Forget();
            return;
            void Action() => a.source.Pause();
        }

        private void ResumeFade(Utils.Scene.AudioSystem.Audio a)
        {
            linkedCts = new CancellationTokenSource();
            linkedCts = CancellationTokenSource.CreateLinkedTokenSource(linkedCts.Token, _cancellationToken);
            a.source.UnPause();
            FadeVolume(a.source, a.source.volume, a.volume, 0.1f, true).Forget();
        }
    }
}