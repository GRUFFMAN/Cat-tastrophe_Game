using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class MainMenu : MonoBehaviour
{
    public GameObject MainMenuUI;
    public GameObject InstructionsUI;
    public GameObject ScoreUI;
    [Header("UI Elements")]
        [SerializeField] public Text scoreText;     // accessing the UI  
        [SerializeField] private GameData gameData = new GameData();
    public string saveFileName = "data.json";  

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
    public void LoadGameData()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, saveFileName);
        if(File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            GameData loadedData = JsonUtility.FromJson<GameData>(dataAsJson);

            scoreText.text = dataAsJson; // adding the score to the UI
        }
        else
        {
            Debug.Log("There is no Data to be Loaded");
        }
    }
}

