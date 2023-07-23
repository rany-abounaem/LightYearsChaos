using System.Collections.Generic;
using UnityEngine;


namespace LightYearsChaos
{
    public class SkillbarUI : MonoBehaviour
    {
        private SkillComponent selectedUnitSkills;
        private List<SkillSlotUI> slots = new List<SkillSlotUI>();

        [SerializeField] private GameObject skillSlotPrefab;
        [SerializeField] private int skillsNumber;

        public void Setup(UIManager manager)
        {
            for (var i = 0; i < skillsNumber; i++)
            {
                var instance = Instantiate(skillSlotPrefab, transform);
                slots.Add(instance.GetComponent<SkillSlotUI>());
            }
            manager.OnUpdateSelection += HandleUnitSkills;
        }


        public void HandleUnitSkills(Unit unit)
        {
            selectedUnitSkills = unit.Skill;
        }
    }
}