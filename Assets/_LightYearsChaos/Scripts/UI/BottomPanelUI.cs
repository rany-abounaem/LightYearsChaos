using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace LightYearsChaos
{
    public class BottomPanelUI : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
    {
        private InputManager input;


        public void Setup(InputManager input)
        {
            this.input = input;
        }


        public void OnPointerEnter(PointerEventData eventData)
        {
            input.ToggleGameInput(false);
        }


        public void OnPointerExit(PointerEventData eventData)
        {
            input.ToggleGameInput(true);
        }
    }
}