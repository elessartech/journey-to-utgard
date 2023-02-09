using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public GameObject MainMenuUI;
    public GameObject OptionsTabUI;
    public GameObject BackgroundMusic;

    private bool options = false;

    void Start()
    {
        MainMenuUI.SetActive(true);
        OptionsTabUI.SetActive(false);
    }

    void Update() {
        if (options)
        {
            MainMenuUI.SetActive(false);
            OptionsTabUI.SetActive(true);
        }
        else {
            MainMenuUI.SetActive(true);
            OptionsTabUI.SetActive(false);
        }
    }

    public void StartGame()
    {
       SceneManager.LoadScene(1); // loading game scene
    }

    public void Options()
    {
       options = !options; // going in and out of that tab
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Music()
    {
        BackgroundMusic.SetActive(false);
    }
}
