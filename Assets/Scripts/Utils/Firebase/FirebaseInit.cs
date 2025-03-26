using Bootstrap;
using Cysharp.Threading.Tasks;
using Firebase.Analytics;
using Firebase.Extensions;
using UnityEngine;

namespace Firebase.Scripts
{
    public class FirebaseInit : IFinishGame
    {
        private readonly LevelController _levelController;


        public FirebaseInit(LevelController levelController)
        {
            _levelController = levelController;
        }

        public async UniTask Initialize()
        {
            await FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
            {
                var dependencyStatus = task.Result;
                if (dependencyStatus == DependencyStatus.Available)
                {
                    // Create and hold a reference to your FirebaseApp,
                    // where app is a Firebase.FirebaseApp property of your application class.
                    FirebaseApp app = FirebaseApp.DefaultInstance;

                    // Set a flag here to indicate whether Firebase is ready to use by your app.
                }
                else
                {
                    Debug.LogError($"Could not resolve all Firebase dependencies: {dependencyStatus}");
                    // Firebase Unity SDK is not safe to use here.
                }
            });
        }

        public void FinishGame()
        {
            FirebaseAnalytics.LogEvent("Completed_Levels", new Parameter[]
            {
                new("Level", _levelController.CurrentLevel)
            });
        }
    }
}