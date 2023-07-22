using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LightYearsChaos
{
    [Serializable]
    public class UnitSensor : MonoBehaviour
    {
        private int teamId;

        public event Action<Unit> OnEnemyDetected;

        public void Setup(int teamId, float boxSize)
        {
            this.teamId = teamId;

            var boxCollider = GetComponent<BoxCollider>();
            boxCollider.size = new Vector3(boxSize, 1, boxSize);
        }


        private void OnTriggerStay(Collider other)
        {
            if (other.TryGetComponent(out Unit unit) && unit.TeamId != teamId)
            {
                OnEnemyDetected?.Invoke(unit);
            }
        }
    }
}

