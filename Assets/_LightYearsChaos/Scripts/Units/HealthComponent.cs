using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LightYearsChaos
{
    public class HealthComponent : MonoBehaviour
    {
        private float health;

        [SerializeField] private float maxHealth;
        [SerializeField] private HealthBar healthBar;

        public event Action<float, float> OnHealthUpdate;
        public event Action OnDeath;


        public void Setup(Unit unit)
        {
            health = maxHealth;
            healthBar.Setup(unit);
        }


        public void Tick(float delta)
        {
            healthBar.Tick(delta);
        }


        public void AddHealth(float value)
        {
            health += value;

            if (health > maxHealth)
            {
                health = maxHealth;
            }

            OnHealthUpdate?.Invoke(health, maxHealth);
        }


        public void RemoveHealth(float value)
        {
            health -= value;

            if (health <= 0)
            {
                health = 0;
                OnDeath?.Invoke();
            }

            OnHealthUpdate?.Invoke(health, maxHealth);
        }
    }

}
