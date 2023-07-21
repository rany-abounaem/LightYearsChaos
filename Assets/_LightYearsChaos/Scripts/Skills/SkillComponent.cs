using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace LightYearsChaos
{
    public class SkillComponent : MonoBehaviour
    {
        private Dictionary<string, Skill> skills = new Dictionary<string, Skill>();


        public void Setup()
        {

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
            }
        }


        public void AddSkill(Skill skill)
        {
            skills.Add(skill.name, skill);
        }


        public void RemoveSkill(Skill skill)
        {
            skills.Remove(skill.name);
        }


        public void CastSkill(string name, Unit target = null)
        {
            skills[name].Cast(target);
        }
    }
}

