using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneManagement : MonoBehaviour
{
    public bool paused;
    public GameObject pausePanel;
    public GameObject statsDisplay;
    public GameObject tutorialPanel;

    private void Awake()
    {
        tutorialPanel.SetActive(false);
        PlayerPrefs.GetInt("tutorialComplete");
        if (PlayerPrefs.GetInt("tutorialComplete") != 1)
        {
            Time.timeScale = 0;
            tutorialPanel.SetActive(true);
        }
    }
    private void Update()
    {
        Inputs();
    }
    public void Pause()
    {
        pausePanel.SetActive(true);
        statsDisplay.SetActive(false);
        paused = true;
        Time.timeScale = 0;
    }
    public void Continue()
    {
        pausePanel.SetActive(false);
        statsDisplay.SetActive(true);
        paused = false;
        Time.timeScale = 1;
        PlayerPrefs.SetInt("tutorialComplete", 1);
    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void LoadScene1()
    {
        SceneManager.LoadScene(1);
    }
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
    public void Inputs()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            if (paused)
            {
                Continue();
            }
            else Pause();

        if (Input.GetKeyDown(KeyCode.P))
        {
            PlayerPrefs.DeleteAll();
        }
    }
}
