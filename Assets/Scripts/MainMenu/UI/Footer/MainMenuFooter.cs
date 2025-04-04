﻿using Bootstrap;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using Utils.UI.Buttons;
using VContainer;

namespace MainMenu.UI.Footer
{
    public class MainMenuFooter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _levelText;
        [SerializeField] private BaseButton _startGameButton;
        [SerializeField] private BaseButton _previousLevelButton;
        [SerializeField] private BaseButton _nextLevelButton;

        private LevelController _levelController;
        private Loader _loader;
        private int _levelNumber;


        [Inject]
        public void Init(LevelController levelController, Loader loader)
        {
            _levelController = levelController;
            _loader = loader;

            _levelNumber = _levelController.CurrentLevel;
        }

        public void UpdateLevel(int newLevel = 0)
        {
            _levelNumber = newLevel > 0 ? newLevel : _levelNumber;
            _levelController.CurrentLevel = _levelNumber;

            CheckButtons();
            _levelText.SetText($"Level {_levelNumber}");
        }

        private void CheckButtons()
        {
            _previousLevelButton.interactable = 1 <= _levelNumber - 1;
            _nextLevelButton.interactable = _levelController.IsNextLevelAvailable();
        }

        private void OnEnable()
        {
            _startGameButton.Init(() => _loader.LoadScene(SceneName.Gameplay).Forget());
            _previousLevelButton.Init(() => UpdateLevel(_levelNumber - 1));
            _nextLevelButton.Init(() => UpdateLevel(_levelNumber + 1));
        }

        private void OnDisable()
        {
            _startGameButton.onClick.RemoveAllListeners();
            _previousLevelButton.onClick.RemoveAllListeners();
            _nextLevelButton.onClick.RemoveAllListeners();
        }
    }
}