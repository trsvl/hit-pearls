﻿using Gameplay.Header;
using Utils.GameSystemLogic.Installers;

namespace Gameplay
{
    public class GameResultChecker
    {
        private ShotsData _shotsData;
        private PearlsData _pearlData;
        private readonly GameplayStateObserver _gameplayStateObserver;


        public GameResultChecker(ShotsData shotsData, PearlsData pearlData, GameplayStateObserver gameplayStateObserver)
        {
            _shotsData = shotsData;
            _pearlData = pearlData;
            _gameplayStateObserver = gameplayStateObserver;
        }

        public void CheckGameResult()
        {
            if (_pearlData.CurrentNumber == )
        }

    }
}