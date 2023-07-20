using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace LightYearsChaos
{
    public class InputManager
    {
        private InputControls inputControls;

        public event Action<Vector2> OnSelect;
        public event Action<Vector2> OnAction;
        public event Action<int> OnCameraZoom;
        public event Action<Vector2> OnCameraPan;


        public InputManager() 
        {
            Setup();
        }


        public void Setup()
        {
            inputControls = new InputControls();
            inputControls.Enable();

            inputControls.Game.Select.performed += (context) =>
            {
                OnSelect?.Invoke(Mouse.current.position.ReadValue());
            };

            inputControls.Game.Action.performed += (context) =>
            {
                OnAction?.Invoke(Mouse.current.position.ReadValue());
            };

            inputControls.Camera.Zoom.performed += (context) =>
            {
                OnCameraZoom?.Invoke(context.ReadValue<Vector2>().y > 0 ? 1 : -1);
            };

            inputControls.Camera.Pan.performed += (context) =>
            {
                OnCameraPan?.Invoke(context.ReadValue<Vector2>());
            };
        }
    }
}

