﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class GameManager : MonoBehaviour
{
    [Header("UI Objects")]
        [SerializeField] public GameObject pauseUI;        // UI object that contains the UI called during Pause
        [SerializeField] public GameObject gameOverUI;     // UI object that contains the UI called during GameOver
        public GameObject InstructionsUI;
        public GameObject controlsUI;
        public GameObject winUI;   
        [SerializeField] public Text scoreText;        // timer used to count down before the gameover menu spawns
    
    //[Header("Game Objects")]
    //    [SerializeField] public GameObject playerGroup;    // Player object that is tracked for when the player dies
    
    
    [Header("Variables")]
        [SerializeField] public bool isGamePaused = false; // bool for tracking if the game is paused
        [SerializeField] public bool isGameOver = false;   // bool for tracking if the Game is over
        [SerializeField] public float timer = 1.2f;        // timer used to count down before the gameover menu spawns

        //public GameObject gerald;

    public Camera cam;

    public Image image;
    public GameObject cat;
    public GameObject pawClosed;

    public GameObject needle1;
    public GameObject endScoreUI;

    public bool gameWin = false;
    public bool isCatCaught = false;
    public int currentScore = 0;
    public int previousScore = 0;

    public static GameManager instance;
    public Slider slider;
    public int gameWinInt = 0;

    public bool level2 = false;
    float levelTimer = 60f;
    int minute = 5;
    string timeLeft;
    public Text TimeText; 
    public GameObject timeUI;
 
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            //currentScore = previousScore;
            levelTimer = 0f;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }

        instance.cam = Camera.main;
        instance.cat = GameObject.FindWithTag("MainCamera"); 

    }

    void Start()
    {

        cam = Camera.main;

        cat = GameObject.FindWithTag("MainCamera");
        
    
        //currentScore = 0;
        //DontDestroyOnLoad(gameObject);
    }
    void FixedUpdate()
    {
        if(level2)
        {
            timeUI.SetActive(true);
            levelTimer -= Time.deltaTime;
            if(levelTimer <= 0f)
            {
                if(minute == 0)
                {
                    isCatCaught = true;
                }
                levelTimer = 59f;
                minute -= 1;
            }
            timeLeft = minute +":" + Mathf.RoundToInt(levelTimer);
            
            TimeText.text = timeLeft;
            //Debug.Log(timeLeft);
        }
    }


    void Update()
    {
        
        
        if(Input.GetKeyDown(KeyCode.Escape)) //if key escape is press
        {
            if(isGameOver == false)          // make sure the game already hasn't ended, as we wouldn't want the pause menu to apear over the gameover menu
            {
                if(gameWin == false)
                {
                    if(isGamePaused == true && (controlsUI.activeSelf == false))     // if the game is paused already
                    {
                        Resume();                // play the game by pressing escape
                    }
                    if(isGamePaused == false && (controlsUI.activeSelf == false))
                    {
                        Pause();                 // pause the game if it isn't
                    }
                }
            }
        }
        
        if(isGameOver == false)                   // if the game hasn't ended
        {
            //isCatCaught = gerald.GetComponent<CatCatching>().isCaught;
            //myobject.GetComponent<myscript>().mybool = true;
            
            
            if(isCatCaught == true)                // if game over is now true, end the game and spawn the game over menu.
            {
                EndGame();
                EndScore(currentScore);
                isGameOver = true;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;

            }
            
            if(gameWinInt > 0 && level2 == true)
            {
                WinGame();
                EndScore(currentScore);
            }
        }
        //scoreText.text = "Score: " + currentScore;
        slider.value = currentScore;
        CrosshairCheck();

        

    }
    ////////////////////////////////////////////// CROSSHAIR /////////////////////////////////////////////////////////
    
    public void CrosshairCheck()
    {
        RaycastHit hit;
        GameObject hitObj;
        float rayDist = 1.0f;

        Vector3 point = cam.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0f));
        Debug.DrawRay(point, cat.transform.forward * rayDist, Color.red);
        // Cast a ray straight downwards.
        if(Physics.Raycast(point, cat.transform.forward, out hit, rayDist))
        {
            hitObj = hit.transform.gameObject;
            if(hitObj.layer == 9)
            {
                //Debug.Log("working");
                image.GetComponent<Image>().color = Color.green; //new Color32(255,255,225,100);
            }
            if(hitObj.layer != 6)
            {
                //Debug.Log("working");
                image.GetComponent<Image>().color = Color.yellow; //new Color32(255,255,225,100);
            }  
        }
        else
        {
            image.GetComponent<Image>().color = new Color32(255,255,225,100);
        } 
    }

    ////////////////////////////////////////////// Controls /////////////////////////////////////////////////////////

    public void SetMouseSensitivity(float sensMultiplier)
    {
        cat.GetComponent<mouse_look>().mouseSensitivity = sensMultiplier;
    }

    ////////////////////////////////////////////// ///////// /////////////////////////////////////////////////////////

    public void EndGame() // if the game is over, make the gameover UI unhidden, Freeze time
    {
        gameOverUI.SetActive(true);
        endScoreUI.SetActive(true);

        pauseUI.SetActive(false);
        Time.timeScale = 0.0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    void Pause() // if the game is paused, make the pause UI unhidden. Freeze time
    {
        pauseUI.SetActive(true);
        Time.timeScale = 0.0f;
        isGamePaused = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        
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
        gameOverUI.SetActive(false);
        endScoreUI.SetActive(false);
        winUI.SetActive(false);
        
        SceneManager.LoadScene(0);
        Destroy(gameObject);

        
    }
    public void Restart() // if restart, reload the active scene.
    {
        gameOverUI.SetActive(false);
        endScoreUI.SetActive(false);
        winUI.SetActive(false);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex,LoadSceneMode.Single);
        //SceneManager.LoadSceneAsync(1);
        Time.timeScale = 1.0f;
        isGamePaused = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.None;
        pauseUI.SetActive(false);
        currentScore = previousScore;
        cat = null;
        cam = null;

        
        //Destroy(gameObject);
    }
    public void QuitGame()
    {
        Application.Quit();
        Cursor.visible = true;
    }
    public void WinGame()
    {
        winUI.SetActive(true);
        endScoreUI.SetActive(true);
        pauseUI.SetActive(false);

        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
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
    public void OpenControls()
    {
        pauseUI.SetActive(false);
        controlsUI.SetActive(true);
    }
    public void CloseContols()
    {
        pauseUI.SetActive(true);
        controlsUI.SetActive(false);
    }

    public void EndScore(int currentScore)
    {
        float precentage = currentScore/12000f;
        float precentageDeg = 208f * precentage;
        var fromRotation = needle1.transform.rotation;
        var toRotation = Quaternion.Euler(0f,0f, 104f - precentageDeg);
        
        needle1.transform.rotation = Quaternion.Slerp(fromRotation,toRotation,Mathf.Sin((precentage - Mathf.Floor(precentage)) * Mathf.PI));

    }

    public void AllQuestsComplete()
    {
        gameWin = true;
    }

}

