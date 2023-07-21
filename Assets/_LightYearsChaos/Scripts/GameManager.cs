using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LightYearsChaos
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private CameraManager cameraManager;

        private InputManager input;
        private PlayerController unitController;

        private void Awake()
        {
            input = new InputManager();
            unitController = new PlayerController(input);
            cameraManager.Setup(input);
        }
    }
}
