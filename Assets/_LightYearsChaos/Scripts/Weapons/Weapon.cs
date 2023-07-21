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
        [SerializeField] private float firingRange;
        [SerializeField] private float fireRate;
        [SerializeField] private bool requiresSkill = false;
        [SerializeField] private Skill skill = null;

        public float Damage { get { return damage; } }
        public float ProjectileSpeed { get { return projectileSpeed; } }
        public float FiringRange { get { return firingRange; } }
        public float FireRate { get { return fireRate; } }
        public bool RequiresSkill { get { return requiresSkill; } }
        public Skill Skill { get { return skill; } set { skill = value; } }


        public bool Fire(Unit target)
        {
            if (requiresSkill)
            {
                return false;
            }

            return true;

        }
    }
}

