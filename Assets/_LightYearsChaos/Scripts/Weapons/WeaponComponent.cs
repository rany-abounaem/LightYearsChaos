using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LightYearsChaos
{
    public class WeaponComponent : MonoBehaviour
    {
        private List<Weapon> weapons = new List<Weapon>();


        public void Setup(List<Weapon> weaponsGiven)
        {
            foreach (var weapon in weaponsGiven)
            {
                Equip(weapon);
            }
        }


        public void Equip(Weapon weapon)
        {
            var unitSkillComponent = GetComponent<Unit>().Skill;

            weapons.Add(weapon);

            if (weapon.RequiresSkill)
            {
                unitSkillComponent.AddSkill(weapon.Skill);
            }
        }
    }
}

