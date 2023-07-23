using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LightYearsChaos
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private CameraManager cameraManager;
        [SerializeField] private UIManager uiManager;
        [SerializeField] private Unit playerUnit;
        [SerializeField] private AIController aiController;
        [SerializeField] private int totalEnemies;
        
        private int deadEnemies;
        private InputManager input;
        private PlayerController playerController;

        public int TotalEnemies { get { return totalEnemies; } }

        public event Action<bool> OnGameOver;
        public event Action<int, int> OnCountUpdate;

        private void Awake()
        {
            input = new InputManager();
            playerController = new PlayerController(input);
            cameraManager.Setup(input);
            uiManager.Setup(this, playerController, input);
            aiController.Setup(totalEnemies);
            aiController.OnDeath += () =>
            {
                deadEnemies++;
                OnCountUpdate?.Invoke(deadEnemies, totalEnemies);

                if (deadEnemies == totalEnemies)
                {
                    OnGameOver?.Invoke(true);
                }
            };

            playerUnit.OnDeath += () =>
            {
                OnGameOver?.Invoke(false);
            };
        }
    }
}
