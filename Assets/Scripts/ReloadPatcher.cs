using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadPatcher : MonoBehaviour
{
    public int previousScore;

    private GameManager gameManager; //Reference your first script here
    public void Start()
    {
        gameManager = (GameManager) GameObject.FindObjectOfType(typeof(GameManager)); //Call the first script
        CopyValues();
        ApplyValues();
        gameManager.cat = GameObject.FindWithTag("MainCamera"); 
        gameManager.cam = Camera.main;
        gameManager.level2 = true;
    }

    public void CopyValues()
    {
        previousScore = gameManager.currentScore;

    }

    public void ApplyValues()
    {
        gameManager.previousScore = previousScore;

    }
}
