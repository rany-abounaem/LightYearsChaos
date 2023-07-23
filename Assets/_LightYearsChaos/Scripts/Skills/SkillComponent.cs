using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace LightYearsChaos
{
    public class SkillComponent : MonoBehaviour
    {
        private Unit unit;
        private Dictionary<string, Skill> skills = new Dictionary<string, Skill>();

        public Dictionary<string, Skill> Skills { get { return skills; } }
        

        public void Setup(Unit unit, List<Skill> skillsGiven)
        {
            this.unit = unit;

            foreach (Skill skill in skillsGiven)
            {
                AddSkill(Instantiate(skill));
            }
        }


        public void Tick(float delta)
        {
            foreach (var entry in skills)
            {
                var skill = entry.Value;
                if (skill.IsOnCooldown && skill.CurrentCooldown > 0)
                {
                    skill.CurrentCooldown -= delta;
                }
                else if (skill.IsOnCooldown && skill.CurrentCooldown <= 0)
                {
                    skill.CurrentCooldown = skill.Cooldown;
                    skill.IsOnCooldown = false;
                }

                var weaponSkill = skill as WeaponSkill;
                if (weaponSkill)
                {
                    if (weaponSkill.IsActive && weaponSkill.CurrentActiveDuration < weaponSkill.MaxActiveDuration)
                    {
                        Debug.Log("Weapon skill activate " + weaponSkill.IsActive + " for duration: " + weaponSkill.CurrentActiveDuration);
                        weaponSkill.CurrentActiveDuration += delta;
                    }
                    else if (weaponSkill.IsActive && weaponSkill.CurrentActiveDuration >= weaponSkill.MaxActiveDuration)
                    {
                        Debug.Log("Deactivated weapon skill");
                        weaponSkill.IsActive = false;
                        weaponSkill.CurrentActiveDuration = 0;
                    }
                    
                }
            }
        }


        public void AddSkill(Skill skill)
        {
            skills.Add(skill.name, skill);
        }


        public void CastSkill(string name, Unit target = null)
        {
            skills[name].Cast(unit, target);
        }

        public void CastSkill(int index, Unit target = null)
        {
            if (index < skills.Count)
            {
                skills.ElementAt(index).Value.Cast(unit, target);
            }
        }
    }
}

