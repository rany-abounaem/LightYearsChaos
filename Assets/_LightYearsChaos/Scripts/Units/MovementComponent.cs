using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

namespace LightYearsChaos
{
    public class MovementComponent : MonoBehaviour
    {
        private NavMeshAgent agent;
        private bool isMoving;
        private bool isRotating;
        private Quaternion fromRotation;
        private Quaternion toRotation;
        private float rotationTime;

        [SerializeField] private float rotationSpeed;

        public event Action<bool> OnMovementUpdate;
        public event Action<bool> OnRotationUpdate;


        public void Setup(NavMeshAgent agent)
        {
            this.agent = agent;

        }


        public void Tick(float delta)
        {
            if (isMoving && !agent.hasPath)
            {
                isMoving = false;
                OnMovementUpdate?.Invoke(false);
            }

            if (isRotating)
            {
                transform.rotation = Quaternion.Slerp(fromRotation, toRotation, rotationTime);
                rotationTime += delta * rotationSpeed;

                if (rotationTime >= 1)
                {
                    isRotating = false;
                    OnRotationUpdate?.Invoke(isRotating);
                } 
            }
        }


        public void Move(Vector3 destination)
        {
            Rotate(destination);

            OnMovementUpdate?.Invoke(true);
            isMoving = true;

            agent.SetDestination(destination);
        }


        public void Rotate(Vector3 dest, bool instant = false)
        {
            var dir = (new Vector3(dest.x, 0, dest.z) - new Vector3(transform.position.x, 0, transform.position.z)).normalized;
            var toRotation = Quaternion.LookRotation(dir);

            if (instant)
            {
                transform.rotation = toRotation;
                OnRotationUpdate?.Invoke(false);
            }
            else
            {
                rotationTime = 0;
                fromRotation = transform.rotation;
                this.toRotation = toRotation;

                isRotating = true;
                OnRotationUpdate?.Invoke(isRotating);
            }
            
        }


        public Vector3 GetClosestPoint(Vector3 targetPosition, float maxDistance)
        {
            var dir = (transform.position - targetPosition).normalized;
            
            return targetPosition + (dir * maxDistance);
        }
    }
}

