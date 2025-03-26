using Bootstrap.Player;
using UnityEngine;

namespace Bootstrap
{
    public class LevelController : IFinishGame
    {
        private readonly SaveManager _saveManager;
        private readonly LevelData _levelData;

        private const string CURRENT_LEVEL = "CurrentLevel";


        public LevelController(SaveManager saveManager)
        {
            _saveManager = saveManager;
            _levelData = _saveManager.Load<LevelData>(CURRENT_LEVEL);

            CurrentLevel = _levelData.UnlockedLevel;
        }

        public int CurrentLevel
        {
            get => _levelData.CurrentLevel;
            set => _levelData.CurrentLevel = LevelData.MaxLevel < value ? LevelData.MaxLevel : value;
        }

        public bool IsMaxLevel()
        {
            return LevelData.MaxLevel == _levelData.CurrentLevel;
        }

        public bool IsNextLevelAvailable()
        {
            return _levelData.CurrentLevel < _levelData.UnlockedLevel;
        }

        public void FinishGame()
        {
            if (IsMaxLevel()) return;
            if (_levelData.CurrentLevel < _levelData.UnlockedLevel) return;

            _levelData.UnlockedLevel++;

            _saveManager.Save(CURRENT_LEVEL, _levelData);
        }
    }
}