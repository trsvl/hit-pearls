using System;

namespace Bootstrap.Player
{
    [Serializable]
    public class VolumeData
    {
        public float SFXVolume = 0f;
        public float MusicVolume = 0f;
    }

    [Serializable]
    public class LevelData
    {
        public const int MaxLevel = 5;
        public int UnlockedLevel = 1;
        public int CurrentLevel = 1;
    }

    [Serializable]
    public class CurrencyData
    {
        public ulong GoldCurrency = 0;
        public ulong DiamondCurrency = 0;
    }
}