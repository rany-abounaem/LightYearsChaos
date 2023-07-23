using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace LightYearsChaos
{
    public class GameOverPanelUI : MonoBehaviour
    {
        [SerializeField] private Button mainMenu;
        [SerializeField] private Button quit;
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private TextMeshProUGUI body;

        [Header("Title & Body Details")]
        [SerializeField] private string victoryTitleText;
        [SerializeField] private string defeatTitleText;
        [SerializeField] private string victoryBodyText;
        [SerializeField] private string defeatBodyText;

        public event Action OnMainMenu;

        public void Setup()
        {
            mainMenu.onClick.AddListener(() =>
            {
                OnMainMenu?.Invoke();
            });

            quit.onClick.AddListener(() => 
            { 
                Application.Quit(); 
            });
        }


        public void SetState(bool state)
        {
            if (state)
            {
                title.text = victoryTitleText;
                body.text = victoryBodyText;
            }
            else
            {
                title.text = defeatTitleText;
                body.text = defeatBodyText;
            }
        }
    }
}