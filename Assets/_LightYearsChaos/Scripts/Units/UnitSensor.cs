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
            boxCollider.center = new Vector3(0, 0, boxSize / 2 + 1);
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

