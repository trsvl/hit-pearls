﻿using UnityEngine;

namespace Bootstrap
{
    public class CameraController
    {
        private Camera _mainCamera;
        private Camera _perspectiveCamera;
        private Camera _uiCamera;

        private float _initialFOV;
        private int alreadyTriggeredIndex;


        public void UpdateCameras(Camera mainCamera, Camera perspectiveCamera, Camera uiCamera)
        {
            _mainCamera = mainCamera;
            _perspectiveCamera = perspectiveCamera;
            _uiCamera = uiCamera;

            _initialFOV = _perspectiveCamera.fieldOfView;
        }

        public float GetInitialFOV()
        {
            return _initialFOV;
        }

        public void AssignNewFOV()
        {
            _initialFOV = _perspectiveCamera.fieldOfView;
        }

        public float CalculateNewFOV(int destroyedSphereLayers)
        {
            const float step = 6f;
            float newFOV = _initialFOV - destroyedSphereLayers * step;

            return newFOV;
        }

        public (Vector3 ballPosition, Vector3 nextBallPosition) UpdateBallsPositionAndFOV(float _ballSize,
            float newFOV = 0f)
        {
            _perspectiveCamera.fieldOfView = newFOV == 0f ? _initialFOV : newFOV;

            float cameraView =
                2.0f * Mathf.Tan(0.5f * Mathf.Deg2Rad * _perspectiveCamera.fieldOfView);
            float distance = 2f * _ballSize / cameraView;
            distance += 0.5f * _ballSize;
            _perspectiveCamera.transform.position = new Vector3(0, 0, distance);

            Vector3 ballPosition = new Vector3(0.8f, 0.2f, distance);
            Vector3 nextBallPosition = new Vector3(0.6f, 0.1f, distance);
            var ballSpawnPoint = _perspectiveCamera.ViewportToWorldPoint(ballPosition);
            var nextBallSpawnPoint = _perspectiveCamera.ViewportToWorldPoint(nextBallPosition);

            return (ballSpawnPoint, nextBallSpawnPoint);
        }

        public Camera GetMainCamera()
        {
            return _mainCamera;
        }

        public Camera GetPerspectiveCamera()
        {
            return _perspectiveCamera;
        }

        public Camera GetUICamera()
        {
            return _uiCamera;
        }
    }
}