using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LightYearsChaos
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private CameraManager cameraManager;
        [SerializeField] private UIManager uiManager;

        private InputManager input;
        private PlayerController playerController;

        private void Awake()
        {
            input = new InputManager();
            playerController = new PlayerController(input);
            cameraManager.Setup(input);
            uiManager.Setup(playerController);
        }
    }
}
