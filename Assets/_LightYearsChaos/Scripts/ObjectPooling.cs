using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LightYearsChaos
{
    public enum PooledObjectType
    {
        SpearProjectile = 0,
        EnchantedSpearProjectile = 0,
        ArrowProjectile,
        FireArrowProjectile,
        COUNT
    }


    public class ObjectPooling : MonoBehaviour
    {
        public static ObjectPooling Instance;
        private Dictionary <int, List<GameObject>> pooledObjects = new Dictionary<int, List<GameObject>> ((int)PooledObjectType.COUNT);

        [SerializeField] private int amountToPool;
        [SerializeField] private List<GameObject> prefabsToPool;

        public Dictionary<int, List<GameObject>> PooledObjects { get { return pooledObjects; } }

        private void Awake()
        {
            Instance = this;
        }


        private void Start()
        {
            for (int i = 0, iMax = prefabsToPool.Count; i < iMax; ++i)
            {
                for (int j = 0; j < amountToPool; ++j)
                {
                    var pooledObj = Instantiate(prefabsToPool[i], transform);
                    pooledObj.SetActive(false);
                    pooledObjects[i].Add(pooledObj);
                }
            }
        }


        public GameObject GetPooledObject(PooledObjectType type)
        {
            var index = (int)type;
            if (index < pooledObjects.Count)
            {
                foreach (var obj in pooledObjects[index])
                {
                    if (!obj.activeInHierarchy)
                    {
                        return obj;
                    }
                }
            }
            return null;
        }
    }
}

