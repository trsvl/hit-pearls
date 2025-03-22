using System.Text;
using Bootstrap.Audio;
using Bootstrap.Currency;
using UnityEngine;
using UnityEngine.Audio;
using Utils.EventBusSystem;
using Utils.Scene.DI;
using Utils.UI;
using VContainer;
using VContainer.Unity;

namespace Bootstrap
{
    public class BootstrapLifetimeScope : BaseLifetimeScope
    {
        [SerializeField] private GameObject _loadingScreenPrefab;
        [SerializeField] private GameObject _currencyPrefab;
        [SerializeField] private AudioMixer _mixer;
        [SerializeField] private SliderObject _sliderPrefab;


        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<Loader>(Lifetime.Singleton)
                .WithParameter(_loadingScreenPrefab);

            builder.Register<PlayerData>(Lifetime.Singleton); //!!!

            builder.Register<CameraController>(Lifetime.Singleton);

            builder.Register<EventBus>(Lifetime.Singleton);

            RegisterCurrency(builder);

            RegisterAudio(builder);
            
            RegisterVolume(builder);

            builder.RegisterEntryPoint<BootstrapEntryPoint>();
            
            builder.RegisterBuildCallback(container =>
            {
                container.Resolve<VolumePresenter>();
            });
        }

        private void RegisterCurrency(IContainerBuilder builder)
        {
            builder.Register<CurrencyView>(Lifetime.Singleton)
                .WithParameter(_currencyPrefab)
                .WithParameter(new CurrencyConverter())
                .WithParameter(new StringBuilder());

            builder.Register<CurrencyModel>(Lifetime.Singleton);

            builder.Register<CurrencyController>(Lifetime.Singleton);
        }


        private void RegisterVolume(IContainerBuilder builder)
        {
            VolumeModel volumeModel = new VolumeModel(_mixer);
            VolumeVIew volumeVIew = new VolumeVIew(_sliderPrefab);
            
            builder.Register<VolumePresenter>(Lifetime.Singleton)
                .WithParameter(volumeModel)
                .WithParameter(volumeVIew);
        }

        private void RegisterAudio(IContainerBuilder builder)
        {
            AudioController audioController = new GameObject("Audio Manager").AddComponent<AudioController>();
            audioController.transform.SetParent(transform);
            builder.RegisterComponent(audioController).WithParameter(audioList.audios);
        }
    }
}