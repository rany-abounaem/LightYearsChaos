using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace LightYearsChaos
{
    public class SkillbarUI : MonoBehaviour
    {
        private SkillComponent selectedUnitSkills;
        private List<SkillSlotUI> slots = new List<SkillSlotUI>();
        private Unit target;

        [SerializeField] private Sprite defaultSkillIcon;
        [SerializeField] private GameObject skillSlotPrefab;
        [SerializeField] private int skillsNumber;


        public void Setup(UIManager manager)
        {
            for (var i = 0; i < skillsNumber; i++)
            {
                var instance = Instantiate(skillSlotPrefab, transform);
                var skillSlotUI = instance.GetComponent<SkillSlotUI>();
                skillSlotUI.Setup(i);
                slots.Add(skillSlotUI);
                skillSlotUI.OnSlotClick += HandleSlotClicked;
            }
            manager.OnSelectionUpdate += HandleSelectionUpdate;
            manager.OnTargetUpdate += HandleTargetUpdate;
        }


        private void HandleSelectionUpdate(Unit unit)
        {
            if (unit != null)
            {
                selectedUnitSkills = unit.Skill;
            }
            else
            {
                selectedUnitSkills = null;
            }
            
            RefreshSlots();
        }


        private void RefreshSlots()
        {
            if (selectedUnitSkills == null)
            {
                foreach (var slot in slots)
                {
                    slot.SkillImage.sprite = defaultSkillIcon;
                }
            }
            else
            {
                var skillsCount = selectedUnitSkills.Skills.Count;
                for (var i = 0; i < slots.Count; i++)
                {
                    var slot = slots[i];
                    if (slot.SlotIndex >= skillsCount)
                    {
                        slot.SkillImage.sprite = defaultSkillIcon;
                    }
                    else
                    {
                        var unitSkill = selectedUnitSkills.Skills.ElementAt(i).Value;
                        slot.SlotSkill = unitSkill;
                        slot.SkillImage.sprite = unitSkill.Icon;
                    }
                }
            }
            
        }


        private void HandleSlotClicked(int slotIndex)
        {
            var slot = slots[slotIndex];

            Debug.Log("Casting");
            selectedUnitSkills.CastSkill(slotIndex, target);
        }

        private void HandleTargetUpdate(Unit target)
        {
            this.target = target;
        }
    }
}