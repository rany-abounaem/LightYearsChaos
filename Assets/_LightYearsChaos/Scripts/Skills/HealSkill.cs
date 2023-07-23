using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LightYearsChaos
{
    [CreateAssetMenu(fileName = "HealSkill", menuName = "ScriptableObjects/Skills/HealSkill")]
    public class HealSkill : Skill
    {
        [SerializeField] private float healValue;
        public override bool Cast(Unit self, Unit target = null)
        {
            if (base.Cast(self, target))
            {
                self.Health.AddHealth(healValue);
                return true;
            }

            return false;
        }
    }
}