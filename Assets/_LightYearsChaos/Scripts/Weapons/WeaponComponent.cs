using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LightYearsChaos
{
    public class WeaponComponent : MonoBehaviour
    {
        private List<Weapon> weapons = new List<Weapon>();
        private SkillComponent skill;

        public List<Weapon> Weapons { get {  return weapons; } }


        public void Setup(List<Weapon> weaponsGiven, SkillComponent skill)
        {
            foreach (var weapon in weaponsGiven)
            {
                this.skill = skill;
                var weaponInstance = Instantiate(weapon);
                Equip(weaponInstance);

            }
        }


        public void Equip(Weapon weapon)
        {
            if (weapon.RequiresSkill)
            {
                var skillInstance = Instantiate(weapon.Skill);
                weapon.Skill = skillInstance;
                skill.AddSkill(skillInstance);
            }
            weapons.Add(weapon);
        }
    }
}

