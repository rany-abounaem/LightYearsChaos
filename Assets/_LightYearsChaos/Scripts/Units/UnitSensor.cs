using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LightYearsChaos
{
    [Serializable]
    public class UnitSensor : MonoBehaviour
    {
        private Unit unit;
        private Coroutine scanCoroutine = null;
        private bool isActive = false;

        public event Action<Unit> OnEnemyDetected;

        public void Setup(Unit unit)
        {
            this.unit = unit;
        }


        public void Activate()
        {
            isActive = true;
            scanCoroutine = StartCoroutine(ScanForEnemies());
        }


        public void Deactivate()
        {
            isActive = false;
            StopCoroutine(scanCoroutine);
        }


        private IEnumerator ScanForEnemies()
        {
            var wait = new WaitForSeconds(0.25f);

            while(isActive)
            {
                var maxActiveWeaponFiringRange = unit.Weapon.GetActiveWeaponsMaxAttackRannge();
                var colliders = Physics.OverlapBox(unit.transform.position, new Vector3(maxActiveWeaponFiringRange, 2, maxActiveWeaponFiringRange));
                foreach (var collider in colliders)
                {
                    if (collider.TryGetComponent(out Unit unit) && (unit.TeamId != this.unit.TeamId))
                    {
                        Debug.Log("Enemy detected by unit " + this.unit.TeamId);
                        OnEnemyDetected?.Invoke(unit);
                    }
                }
                yield return wait;
            }
            
        }
    }
}