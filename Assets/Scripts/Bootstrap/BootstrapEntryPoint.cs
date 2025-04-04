﻿using System.Threading;
using Bootstrap.Audio;
using Cysharp.Threading.Tasks;
using Firebase.Scripts;
using VContainer;
using VContainer.Unity;

namespace Bootstrap
{
    public class BootstrapEntryPoint : IAsyncStartable
    {
        private readonly IObjectResolver _container;


        public BootstrapEntryPoint(IObjectResolver container)
        {
            _container = container;
        }

        public async UniTask StartAsync(CancellationToken cancellation)
        {
            await _container.Resolve<FirebaseInit>().Initialize();

            _container.Resolve<VolumePresenter>().Initialize();

            bool isBootstrapScene = Loader.IsCurrentSceneEqual(SceneName.Bootstrap);


            if (!isBootstrapScene) return;

            var loader = _container.Resolve<Loader>();
            await loader.LoadScene(SceneName.MainMenu);
        }
    }
}