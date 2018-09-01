using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneManagement : MonoBehaviour
{
    public bool paused;
    public GameObject pausePanel;
    public GameObject statsDisplay;


    private void Start()

    {
        Time.timeScale = 1;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            if (paused)
            {
                Continue();
            }
            else Pause();
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
}
