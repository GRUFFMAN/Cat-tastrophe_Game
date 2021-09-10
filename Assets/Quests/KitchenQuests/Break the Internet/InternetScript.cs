using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Quests;

public class InternetScript : MonoBehaviour
{
    private int state = 0;

    public GameObject meme1;
    public GameObject meme2;
    public GameObject meme3;
    public GameObject meme4;
    public GameObject meme5;
    public GameObject meme6;

    bool touched = false;
    bool keyboard = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {        
        switch(state)
        {
            case 0:
            {
                if(touched == true)
                {
                    touched = false;
                    
                    meme1.SetActive(true);
                    
                    state +=2;
                }
                break;
            }
            case 2:
            {
                if(touched == true)
                {
                    touched = false;
                    meme2.SetActive(true);
                    
                    state +=1;
                }
                break;
            }
            case 3:
            {
                if(touched == true)
                {
                    touched = false;
                    meme3.SetActive(true);
                    
                    state +=1;
                }
                break;
            }
            case 4:
            {
                if(touched == true)
                {
                    touched = false;
                    meme4.SetActive(true);
                   
                    state +=1;
                }
                break;
            }
            case 5:
            {
                if(touched == true)
                {
                    touched = false;
                    meme5.SetActive(true);

                    state +=1;
                }
                break;
            }
            case 6:
            {
                if(touched == true)
                {
                    meme6.SetActive(true);
                    touched = false;

                    // finish quest
                    QuestManager.instance.SetQuestComplete("internetCat");

                    state +=1;
                }
                break;
            }
            default:
            {
                break;
            }
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player" && keyboard == false)
        {
            touched = true;
            keyboard = true;
        }
        //Debug.Log("keyboard");
    }

    void OnTriggerExit(Collider col)
    {
        if(col.tag == "Player")
        {
            keyboard = false;
        }
    }
}