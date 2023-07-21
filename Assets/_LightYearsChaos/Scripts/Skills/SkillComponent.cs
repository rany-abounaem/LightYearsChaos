using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace LightYearsChaos
{
    public class SkillComponent : MonoBehaviour
    {
        private Dictionary<string, Skill> skills = new Dictionary<string, Skill>();


        public void Setup(List<Skill> skillsGiven)
        {
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
                else if (skill.IsOnCooldown && skill.Cooldown <= 0)
                {
                    skill.CurrentCooldown = skill.Cooldown;
                    skill.IsOnCooldown = false;
                }

                var weaponSkill = skill as WeaponSkill;
                if (weaponSkill)
                {
                    if (weaponSkill.IsActive && weaponSkill.CurrentActiveDuration < weaponSkill.MaxActiveDuration)
                    {
                        weaponSkill.CurrentActiveDuration += delta;
                    }
                    else if (weaponSkill.IsActive && weaponSkill.CurrentActiveDuration >= weaponSkill.MaxActiveDuration)
                    {
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
            skills[name].Cast(target);
        }

        public void CastSkill(int index, Unit target = null)
        {
            if (index < skills.Count)
            {
                skills.ElementAt(index).Value.Cast(target);
            }
        }
    }
}

