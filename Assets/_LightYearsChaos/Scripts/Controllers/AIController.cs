using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LightYearsChaos
{
    public class AIController : MonoBehaviour
    {
        private int totalUnits;
        private int deadUnits;
        private List<Unit> controlledUnits = new List<Unit>();

        [SerializeField] private Vector3 spawnLocation;
        [SerializeField] private Unit playerUnit;
        [SerializeField] private GameObject unitPrefab;

        public event Action OnDeath;


        public void Setup(int totalUnits)
        {
            //GameObject pooledUnitObject = null;
            //pooledUnitObject = ObjectPooling.Instance.GetPooledObject(PooledObjectType.MagicianUnit);
            //if (pooledUnitObject == null)
            //{
            //    yield return new WaitForSeconds(2f);
            //    pooledUnitObject = ObjectPooling.Instance.GetPooledObject(PooledObjectType.MagicianUnit);
            //}  
            //Debug.Log("pooled object" + pooledUnitObject);

            this.totalUnits = totalUnits;
            for (var i = 0; i < totalUnits; i++)
            {
                var unitObj = Instantiate(unitPrefab, transform);
                unitObj.SetActive(false);
                var unit = unitObj.GetComponent<Unit>();
                controlledUnits.Add(unit);

            }
            SpawnUnit();
        }


        public void SpawnUnit()
        {
            var controlledUnit = controlledUnits[deadUnits];
            RandomizePosition(controlledUnit);
            controlledUnit.gameObject.SetActive(true);
            controlledUnit.transform.position = spawnLocation;
            controlledUnit.StateManager.SetState(new ChaseState(controlledUnit, controlledUnit.StateManager, playerUnit));
            controlledUnit.OnDeath += () =>
            {
                deadUnits++;

                OnDeath?.Invoke();

                if (deadUnits < totalUnits)
                {
                    SpawnUnit();
                }
            };
        }


        private void RandomizePosition(Unit unit)
        {
            unit.gameObject.transform.position = spawnLocation + new Vector3(UnityEngine.Random.Range(-10f, 10f), 0, UnityEngine.Random.Range(-10f, 10f));
        }
    }
}

