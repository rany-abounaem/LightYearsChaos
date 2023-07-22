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
            var currentBarCount = (int)((health / maxHealth) * totalBarCount);

            for (int i = totalBarCount - 1; i > currentBarCount; i--)
            {
                healthBars[i].color = Color.grey;
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

