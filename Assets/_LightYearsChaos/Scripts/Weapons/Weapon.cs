using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LightYearsChaos
{
    public class Weapon : ScriptableObject
    {
        private float damage;
        private float projectileSpeed;
        private float fireRate;
        private bool requiresSkill = false;
        private Skill skill = null;

        public bool RequiresSkill { get { return requiresSkill; } }
        public Skill Skill { get { return skill; } }


        public bool Fire()
        {
            if (requiresSkill)
            {
                return false;
            }

            return true;

        }
    }
}

