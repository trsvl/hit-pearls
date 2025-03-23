using System.Threading;
using Cysharp.Threading.Tasks;
using MainMenu.Animations;
using MainMenu.UI.Footer;
using MainMenu.UI.Header;
using Utils.EventBusSystem;
using VContainer;
using VContainer.Unity;

namespace MainMenu.DI
{
    public class MainMenuEntryPoint : IAsyncStartable
    {
        private readonly IObjectResolver _container;


        public MainMenuEntryPoint(IObjectResolver container)
        {
            _container = container;
        }

        public void Initialize()
        {
        }

        public async UniTask StartAsync(CancellationToken cancellation = new())
        {
            _container.Resolve<MainMenuHeaderManager>().InitHeader();
            _container.Resolve<MainMenuFooter>().UpdateLevel();

            await _container.Resolve<MoveUIAnimation>().MoveOnStart();

            _container.Resolve<EventBus>().RaiseEvent<IMainMenuStart>(handler => handler.OnMainMenuStart());
        }
    }
}