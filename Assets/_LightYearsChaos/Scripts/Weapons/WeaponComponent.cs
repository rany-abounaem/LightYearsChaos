using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LightYearsChaos
{
    public class WeaponComponent : MonoBehaviour
    {
        private List<Weapon> weapons = new List<Weapon>();
        private SkillComponent skill;
        private float maxFiringRange = 0;
        private bool isAttacking = false;
        private Unit unit;

        public List<Weapon> Weapons { get {  return weapons; } }
        public float MaxFiringRange { get { return maxFiringRange; } }


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
                if (weapon.RequiresSkill && !((WeaponSkill)weapon.Skill).IsActive)
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
                StartCoroutine(AttackinngCoroutine(weapon, target));
            }
        }


        private IEnumerator AttackinngCoroutine(Weapon weapon, Unit target)
        {
            var wait = new WaitForSeconds(weapon.FireRate);

            while (isAttacking)
            {
                weapon.Use(unit, target);
                yield return wait;
            }
        }


        public void DeactivateFiring()
        {
            isAttacking = false;
            StopAllCoroutines();
        }
    }
}