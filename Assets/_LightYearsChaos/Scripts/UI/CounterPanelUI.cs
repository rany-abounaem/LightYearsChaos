using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


namespace LightYearsChaos
{
    public class CounterPanelUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI counter;


        public void Setup(GameManager gameManager)
        {
            gameManager.OnCountUpdate += (current, total) =>
            {
                counter.text = "Enemies Killed<br>" + current + " / " + total;
            };
        }
    }
}