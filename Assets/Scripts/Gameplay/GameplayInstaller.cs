﻿using System.Collections;
using Gameplay.Animations;
using Gameplay.Animations.EntryPoint;
using Gameplay.BallThrowing;
using Gameplay.Header;
using Gameplay.SphereData;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils.UI.Buttons;

namespace Gameplay
{
    public class GameplayInstaller : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI pearlsText;
        [SerializeField] private TextMeshProUGUI shotsText;
        [SerializeField] private BallThrower ballThrower;
        [SerializeField] private SphereOnHitBehaviour _sphereOnHitBehaviour;
        [SerializeField] private Button pauseButton;
        [SerializeField] private Button changeBallButton;

        [Header("Popup")] [Space] [SerializeField]
        private TextButton firstButton;

        [SerializeField] private TextButton secondButton;
        [SerializeField] private GameObject popup;

        private GameplayStateObserver gameplayStateObserver;


        private IEnumerator Start()
        {
            var spherePrefab = Resources.Load<GameObject>("Prefabs/Sphere");
            var dataContext = new DataContext();
            gameplayStateObserver = new GameplayStateObserver();
            var gamePopup = new GamePopup(popup, firstButton, secondButton, gameplayStateObserver);
            var allColors = new AllColors();
            var spheresDictionary = new SpheresDictionary();

            var sphereGenerator = new GameObject().AddComponent<SphereGenerator>();
            sphereGenerator.Init(spherePrefab, allColors, spheresDictionary);

            int level = 1;
            
            
            
            
            yield return StartCoroutine(dataContext.LoadSpheres(level, sphereGenerator));

            
            
            
            
            int shotsCount = sphereGenerator._levelColors.Length * 2;
            var shotsData = new ShotsData(shotsText, shotsCount, gameplayStateObserver);

          

            gameplayStateObserver.AddListener(sphereGenerator);
            gameplayStateObserver.AddListener(ballThrower);
            gameplayStateObserver.AddListener(gamePopup);


            gameplayStateObserver.StartGame();
        }

        private void UpdateCameraFOV()
        {
        }

        private void OnEnable()
        {
            pauseButton.onClick.AddListener(() => gameplayStateObserver.PauseGame());
          //  changeBallButton.onClick.AddListener(() => ballThrower.RespawnBall());
        }

        private void OnDisable()
        {
            pauseButton.onClick.RemoveAllListeners();
            changeBallButton.onClick.RemoveAllListeners();
        }
    }
}