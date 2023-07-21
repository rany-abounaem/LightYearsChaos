using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace LightYearsChaos
{
    public class MovementComponent : MonoBehaviour
    {
        private NavMeshAgent agent;
        private bool isMoving;

        public event Action<bool> OnMovementUpdate;


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
        }


        public void Move (Vector3 destination)
        {
            OnMovementUpdate?.Invoke(true);
            isMoving = true;
            agent.SetDestination(destination);
        }
    }
}

