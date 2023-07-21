using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LightYearsChaos
{
    [CreateAssetMenu (fileName = "WeaponSkill", menuName = "ScriptableObjects/Skills/WeaponSkill")]
    public class WeaponSkill : Skill
    {
        [SerializeField] private bool isActive;
        [SerializeField] private float maxActiveDuration;
        [SerializeField] private float currentActiveDuration;

        public bool IsActive { get { return isActive; } set { isActive = value; } }
        public float MaxActiveDuration { get { return maxActiveDuration; } }
        public float CurrentActiveDuration { get { return currentActiveDuration; } set { currentActiveDuration = value; } }


        public override bool Cast(Unit target = null)
        {
            if (base.Cast(target))
            {
                isActive = true;
                return true;
            }

            return false;
        }
    }
}

