using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace LightYearsChaos
{
    public class MainMenuUI : MonoBehaviour
    {
        private bool isTransitioning;
        private float transitionTime;

        [SerializeField] private Button play;
        [SerializeField] private Button exit;
        [SerializeField] private Image fadeIn;


        private void Awake()
        {
            play.onClick.AddListener(() =>
            {
                StartCoroutine(LoadMainScene());
            });

            exit.onClick.AddListener(() =>
            {
                Application.Quit();
            });
        }


        private void Start()
        {
            fadeIn.color = new Color(0f, 0f, 0f, 0f);
        }


        private void Update()
        {
            if (isTransitioning && transitionTime <= 1f)
            {
                transitionTime += Time.deltaTime;
                fadeIn.color = new Color(0f, 0f, 0f, transitionTime);
            }
        }


        private IEnumerator LoadMainScene()
        {
            isTransitioning = true;
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene(1);
            AudioManager.instance.Stop("MainMenu");
            AudioManager.instance.Play("Background");
        }
    }
}