using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LightYearsChaos
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private CameraManager cameraManager;

        private InputManager input;
        private UnitController unitController;

        private void Awake()
        {
            input = new InputManager();
            unitController = new UnitController(input);
            cameraManager.Setup(input);
        }
    }
}
