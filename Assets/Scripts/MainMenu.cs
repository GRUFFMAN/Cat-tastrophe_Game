using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class UIController : MonoBehaviour
{
    public GameObject MainMenuUI;
    public GameObject InstructionsUI;
    public GameObject ScoreUI;

    void start()
    {
        
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 1.0f;
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void OpenInstructions()
    {
        MainMenuUI.SetActive(false);
        InstructionsUI.SetActive(true);
    }
    public void closeInstructions()
    {
        MainMenuUI.SetActive(true);
        InstructionsUI.SetActive(false);
    }
    public void OpenScore()
    {
        MainMenuUI.SetActive(false);
        ScoreUI.SetActive(true);
    }
    public void closeScore()
    {
        MainMenuUI.SetActive(true);
        ScoreUI.SetActive(false);
    }

}

