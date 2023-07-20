using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace LightYearsChaos
{
    public class Movement : MonoBehaviour
    {
        private NavMeshAgent agent;


        public void Setup(NavMeshAgent agent)
        {
            this.agent = agent;
        }


        public void Move (Vector3 destination)
        {
            agent.SetDestination(destination);
        }
    }
}

