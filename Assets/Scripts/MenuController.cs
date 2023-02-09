using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject PauseMenuUI;
    public GameObject GameOverUI;
    public GameObject WinPanelUI;

    public bool isGameOver = false;
    public bool isGameWon = false;
    private bool paused = false;

    void Start()
    {
        PauseMenuUI.SetActive(false);
        GameOverUI.SetActive(false);
        WinPanelUI.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;
        }

        if (paused && !isGameOver && !isGameWon)
        {
            PauseMenuUI.SetActive(true); // enabling UI element
            Time.timeScale = 0; // freezing the time
        }

        if (!paused && !isGameOver && !isGameWon)
        {
            PauseMenuUI.SetActive(false); // disabling UI element 
            Time.timeScale = 1; // resuming the time in game
        }
    }

    public void ShowWinPanel()
    {
        WinPanelUI.SetActive(true);
        Time.timeScale = 0; // freezing the time
    }
    public void ShowGameOver()
    {
        GameOverUI.SetActive(true); // enabling UI element
    }

    /* ****************************** PAUSE MENU FUNCTIONALITY RELATED METHODS ****************************** */
    public void Resume()
    {
        paused = false;
    }

    public void Restart()
    {
       SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
