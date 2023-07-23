using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LightYearsChaos
{
    public class HealthBar : MonoBehaviour
    {
        private bool isMoving;
        private bool isRotating;

        [SerializeField] private List<SpriteRenderer> healthBars = new List<SpriteRenderer>();


        public void Setup(Unit unit)
        {
            unit.Movement.OnMovementUpdate += HandleMovement;
            unit.Movement.OnRotationUpdate += HandleRotation;
            unit.Health.OnHealthUpdate += UpdateHealthBars;

            var camera = Camera.main.GetComponent<CameraManager>();
            camera.OnCameraUpdate += UpdateTransform;
            UpdateTransform();

            UpdateHealthBars(unit.Health.Health, unit.Health.MaxHealth);
        }


        public void Tick(float delta)
        {
            if (isMoving || isRotating) 
            {
                UpdateTransform();
            }
        }


        private void UpdateTransform()
        {
            var dir = Camera.main.transform.position - transform.position;
            var rot = Quaternion.LookRotation(dir);
            transform.rotation = rot;
        }


        private void UpdateHealthBars(float health, float maxHealth)
        {
            var totalBarCount = healthBars.Count;
            var currentBarCount = (int)Mathf.Ceil((health / maxHealth) * totalBarCount);

            for (int i = 0; i < currentBarCount; i++)
            {
                healthBars[i].color = Color.green;
            }
            for (int i = currentBarCount; i < totalBarCount; i++)
            {
                healthBars[i].color = Color.gray;
            }
        }


        private void HandleMovement(bool state)
        {
            isMoving = true;
        }

        private void HandleRotation(bool state)
        {
            isRotating = true;
        }
    }
}