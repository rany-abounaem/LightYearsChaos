using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LightYearsChaos
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "ScriptableObjects/Weapons/Weapon")]
    public class Weapon : ScriptableObject
    {
        [SerializeField] private Sprite icon;
        [SerializeField] private float damage;
        [SerializeField] private float projectileSpeed;
        [SerializeField] private float attackRange;
        [SerializeField] private float attackRate;
        [SerializeField] private bool requiresSkill = false;
        [SerializeField] private WeaponSkill skill = null;
        [SerializeField] private PooledObjectType projectileType;

        public float Damage { get { return damage; } }
        public float ProjectileSpeed { get { return projectileSpeed; } }
        public float AttackRange { get { return attackRange; } }
        public float AttackRate { get { return attackRate; } }
        public bool RequiresSkill { get { return requiresSkill; } }
        public WeaponSkill Skill { get { return skill; } set { skill = value; } }


        public virtual bool Use(Unit self, Unit target)
        {
            if (requiresSkill && !skill.IsActive)
            {
                return false;
            }

            var pooledObject = ObjectPooling.Instance.GetPooledObject(projectileType);
            pooledObject.SetActive(true);
            pooledObject.transform.position = self.transform.position;
            var dir = (target.transform.position - pooledObject.transform.position).normalized;
            var lookRotation = Quaternion.LookRotation(dir);
            pooledObject.transform.rotation = lookRotation;
            var projectile = pooledObject.GetComponent<Projectile>();
            projectile.SendProjectile(target, damage, projectileSpeed);

            return true;

        }
    }
}