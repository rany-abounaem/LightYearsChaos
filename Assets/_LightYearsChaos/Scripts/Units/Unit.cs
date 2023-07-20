using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace LightYearsChaos
{
    public class Unit : MonoBehaviour
    {
        private NavMeshAgent agent;
        private Movement movement;
        private int teamId;
        [SerializeField] private GameObject selectionObject;

        public Movement Movement { get { return movement; } }
        public int TeamId { get { return teamId; } }
        public GameObject SelectionObject { get {  return selectionObject; } }

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            movement = GetComponent<Movement>();
            movement.Setup(agent);
        }
    }
}