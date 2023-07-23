using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LightYearsChaos
{
    public abstract class Skill : ScriptableObject
    {
        [SerializeField] private Sprite icon;
        [SerializeField] private bool isOnCooldown;
        [SerializeField] private float cooldown;
        [SerializeField] private float currentCooldown;

        public event Action<bool> OnCooldown;

        public Sprite Icon { get { return icon; } }
        public bool IsOnCooldown { get { return isOnCooldown; } set { isOnCooldown = value; OnCooldown?.Invoke(value); } }
        public float Cooldown { get { return cooldown; }  }
        public float CurrentCooldown { get { return currentCooldown; } set { currentCooldown = value; } }

        public virtual bool Cast(Unit self, Unit target = null)
        {
            if (isOnCooldown)
            {
                return false;
            }

            IsOnCooldown = true;

            return true;
        }
    }
}

