using Bootstrap.Currency;
using Cysharp.Threading.Tasks;
using Gameplay.Animations;

namespace Gameplay.Utils
{
    public class FinishGameController
    {
        private readonly MoveUIAnimation _moveUIAnimation;
        private readonly CurrencyController _currencyController;


        public FinishGameController(MoveUIAnimation moveUIAnimation, CurrencyController currencyController)
        {
            _moveUIAnimation = moveUIAnimation;
            _currencyController = currencyController;
        }

        public async UniTask FinishGame()
        {
            await _moveUIAnimation.ChangeHeader(0.25f);
            _currencyController.AddCurrency(CurrencyType.Gold, 500);
        }
    }
}