using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static System.TimeZoneInfo;

namespace LightYearsChaos
{
    public class UIManager : MonoBehaviour
    {
        private bool isTransitioning;
        private float transitionTime;

        [SerializeField] private SkillbarUI skillbar;
        [SerializeField] private BottomPanelUI bottomPanel;
        [SerializeField] private CounterPanelUI counterPanel;
        [SerializeField] private GameOverPanelUI gameOverPanel;
        [SerializeField] private Image fadeIn;

        public event Action<Unit> OnSelectionUpdate;
        public event Action<Unit> OnTargetUpdate;

        public void Setup (GameManager gm, PlayerController playerController, InputManager inputManager)
        {
            playerController.OnSelectionUpdate += (unit) =>
            {
                OnSelectionUpdate?.Invoke(unit);
            };

            playerController.OnTargetUpdate += (unit) =>
            {
                OnTargetUpdate?.Invoke(unit);
            };

            gm.OnGameOver += (bool state) =>
            {
                ShowGameOverPanel(state);
            };

            bottomPanel.Setup(inputManager);
            skillbar.Setup(this);
            gameOverPanel.Setup();
            counterPanel.Setup(gm);

            fadeIn.color = new Color(0f, 0f, 0f, 0f);
        }


        private void Update()
        {
            if (isTransitioning) 
            {
                transitionTime += Time.deltaTime;
                fadeIn.color = new Color(0f, 0f, 0f, transitionTime);
            }
        }


        private void ShowGameOverPanel(bool state)
        {
            gameOverPanel.gameObject.SetActive(true);
            gameOverPanel.SetState(state);
            gameOverPanel.OnMainMenu += () =>
            {
                StartCoroutine(LoadMainMenuScene());
            };
        }


        private IEnumerator LoadMainMenuScene()
        {
            isTransitioning = true;
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene(0);
            AudioManager.instance.Stop("Background");
            AudioManager.instance.Play("MainMenu");
        }
    }
}