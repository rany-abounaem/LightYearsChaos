using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LightYearsChaos
{
    public class WeaponComponent : MonoBehaviour
    {
        private List<Weapon> weapons = new List<Weapon>();
        private SkillComponent skill;
        private bool isAttacking = false;
        private Unit unit;

        public List<Weapon> Weapons { get {  return weapons; } }


        public void Setup(Unit unit, List<Weapon> weaponsGiven, SkillComponent skill)
        {
            this.unit = unit;

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


        public float GetActiveWeaponsMaxAttackRannge()
        {
            float maxFiringRange = 0;

            foreach (var weapon in weapons)
            {
                if (weapon.RequiresSkill && !weapon.Skill.IsActive)
                {
                    continue;
                }
                else
                {
                    maxFiringRange = Mathf.Max(maxFiringRange, weapon.AttackRange);
                }
            }

            return maxFiringRange;
        }


        public void ActivateAttacking(Unit target)
        {
            isAttacking = true;

            foreach (var weapon in weapons)
            {
                StartCoroutine(AttackingCoroutine(weapon, target));
            }
        }

        public void DeactivateFiring()
        {
            isAttacking = false;
            StopAllCoroutines();
        }


        private IEnumerator AttackingCoroutine(Weapon weapon, Unit target)
        {
            var wait = new WaitForSeconds(weapon.AttackRate);

            while (isAttacking)
            {
                weapon.Use(unit, target);
                yield return wait;
            }
        }
    }
}