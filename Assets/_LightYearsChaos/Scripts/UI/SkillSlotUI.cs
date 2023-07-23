using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LightYearsChaos
{
    public class SkillSlotUI : MonoBehaviour
    {
        private int slotIndex;

        [SerializeField] private TextMeshProUGUI hotkeyText;
        [SerializeField] private Image skillImage;

        public int SlotIndex { get { return slotIndex; } }


    }
}