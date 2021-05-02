using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class GameManager : MonoBehaviour
{
    [Header("UI Objects")]
        [SerializeField] public GameObject pauseUI;        // UI object that contains the UI called during Pause
        [SerializeField] public GameObject gameOverUI;     // UI object that contains the UI called during GameOver
        public GameObject InstructionsUI;
        public GameObject winUI;   
    
    [Header("Game Objects")]
        [SerializeField] public GameObject playerGroup;    // Player object that is tracked for when the player dies
    
    
    [Header("Variables")]
        [SerializeField] public bool isGamePaused = false; // bool for tracking if the game is paused
        [SerializeField] public bool isGameOver = false;   // bool for tracking if the Game is over
        [SerializeField] public float timer = 1.2f;        // timer used to count down before the gameover menu spawns

    bool gameWin = false;

    void Start()
    {

    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) //if key escape is press
        {
            if(isGameOver == false)          // make sure the game already hasn't ended, as we wouldn't want the pause menu to apear over the gameover menu
            {
                if(gameWin == false)
                {
                    if(isGamePaused == true)     // if the game is paused already
                    {
                        Resume();                // play the game by pressing escape
                    }
                    else
                    {
                        Pause();                 // pause the game if it isn't
                    }
                }
            }
        }
        
        if(isGameOver == false)                   // if the game hasn't ended
        {
            if(playerGroup.activeSelf == false)   // if the player has died (player model gets hidden)
            {
                timer -= (1*Time.deltaTime);      // start counting down the timer
                if(timer <= 0)
                {   
                    isGameOver = true;            // if the timer reaches 0, tell the bool that the game is over
                }
            }
            if(isGameOver == true)                // if game over is now true, end the game and spawn the game over menu.
            {
                EndGame();
            }
            if(gameWin == true)
            {
                WinGame();
            }
        }
    }
    public void EndGame() // if the game is over, make the gameover UI unhidden, Freeze time
    {
        gameOverUI.SetActive(true);
        Time.timeScale = 0.0f;
        Cursor.visible = true;
    }

    void Pause() // if the game is paused, make the pause UI unhidden. Freeze time
    {
        pauseUI.SetActive(true);
        Time.timeScale = 0.0f;
        isGamePaused = true;
        Cursor.visible = true;
    }
    public void Resume() // if resume, reset time to full and hide the pause menu, relock the cursor for gameplay
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1.0f;
        isGamePaused = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }
    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void Restart() // if restart, reload the active scene.
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex,LoadSceneMode.Single);
        Time.timeScale = 1.0f;
        isGamePaused = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }
    public void QuitGame()
    {
        Application.Quit();
        Cursor.visible = true;
    }
    public void WinGame()
    {
        winUI.SetActive(true);
        Time.timeScale = 0f;
        Cursor.visible = true;
    }
    public void OpenInstructions()
    {
        pauseUI.SetActive(false);
        InstructionsUI.SetActive(true);
    }
    public void closeInstructions()
    {
        pauseUI.SetActive(true);
        InstructionsUI.SetActive(false);
    }

}

