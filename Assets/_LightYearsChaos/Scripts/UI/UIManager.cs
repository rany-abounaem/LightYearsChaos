using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LightYearsChaos
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private SkillbarUI skillbarUI;
        [SerializeField] private BottomPanelUI bottomPanelUI;

        public event Action<Unit> OnSelectionUpdate;
        public event Action<Unit> OnTargetUpdate;

        public void Setup (PlayerController playerController, InputManager inputManager)
        {
            playerController.OnSelectionUpdate += (unit) =>
            {
                OnSelectionUpdate?.Invoke(unit);
            };

            playerController.OnTargetUpdate += (unit) =>
            {
                OnTargetUpdate?.Invoke(unit);
            };

            bottomPanelUI.Setup(inputManager);
            skillbarUI.Setup(this);
        }
    }
}