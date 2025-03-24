using UnityEngine;

namespace Bootstrap
{
    public class PlayerData
    {
        public int CurrentLevel
        {
            get => currentLevel;
            set
            {
                currentLevel = maxLevel < value ? maxLevel : value;

                PlayerPrefs.SetInt(CURRENT_LEVEL, currentLevel);
            }
        }

        private const int maxLevel = 5; //!!!

        private int currentLevel = PlayerPrefs.GetInt(CURRENT_LEVEL, 1);
        private const string CURRENT_LEVEL = "CurrentLevel";

        public bool IsMaxLevel()
        {
            return maxLevel == currentLevel;
        }
    }
}