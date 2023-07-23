using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace LightYearsChaos
{
    public class SkillSlotUI : MonoBehaviour, IPointerDownHandler
    {
        private int slotIndex;
        private Skill slotSkill;

        [SerializeField] private TextMeshProUGUI hotkeyText;
        [SerializeField] private Image skillImage;

        public int SlotIndex { get { return slotIndex; } }
        public Image SkillImage { get { return skillImage; } }
        public Skill SlotSkill { get { return slotSkill; } set { if (slotSkill != null) { slotSkill.OnCooldown -= UpdateImage; }  slotSkill = value; if (value != null) { slotSkill.OnCooldown += UpdateImage; } } }

        public event Action<int> OnSlotClick;


        public void Setup(int slotIndex)
        {
            this.slotIndex = slotIndex;
            hotkeyText.text = (slotIndex + 1).ToString();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            OnSlotClick?.Invoke(slotIndex);
        }

        private void UpdateImage(bool cooldown)
        {
            skillImage.color = cooldown ? Color.grey : Color.white;
        }
    }
}