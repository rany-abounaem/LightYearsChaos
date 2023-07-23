using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LightYearsChaos
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private SkillbarUI skillbarUI;

        private PlayerController playerController;

        public event Action<Unit> OnUpdateSelection;

        public void Setup (PlayerController playerController)
        {
            playerController.OnUpdateSelection += (unit) =>
            {
                OnUpdateSelection?.Invoke(unit);
            };

            skillbarUI.Setup(this);
        }
    }
}