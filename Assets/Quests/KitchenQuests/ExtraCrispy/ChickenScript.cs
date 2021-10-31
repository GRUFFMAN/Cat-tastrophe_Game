using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Quests;

public class ChickenScript : MonoBehaviour
{
    public GameObject cat;
    public GameObject chicken;
    public GameObject ovenDial;

    public AudioSource oven;
    public AudioClip humm;
    public AudioClip click;

    public GameObject eInteract;
    float timeRemaining;
    private int state = 0;

    public Material m_Material;
    Color chickenSkin;
    Renderer rend;
    
    void Start()
    {
        rend = chicken.GetComponent<Renderer>();
        oven.clip = humm;
        
        chickenSkin = rend.material.color;//new Color(214f,152f,40f);
        //m_Material.color = chickenSkin;
        
        
    }
    
    // Update is called once per frame
    void Update()
    {
        switch(state)
        {
            case 0:
            case 1:
            case 2: // await and count e's // 3 e's // basically cranking the oven dials
            {
                
                if(Vector3.Distance(cat.transform.position, ovenDial.transform.position) < 1f)
                {
                    eInteract.SetActive(true);
                    
                    
                    if(Input.GetKeyDown(KeyCode.E))
                    {
                        eInteract.SetActive(false);
                        Debug.Log(state);
                        // play click sound
                        state += 1;
                    }
                }
                else
                {
                    eInteract.SetActive(false);
                }
                break;
            }
            case 3: // oven turns on, chicken visibly cooks
            {
                // play dial tone
                //oven.clip = humm;
                //oven.Play();

                Debug.Log("oven on");
                timeRemaining = 0f;
                oven.Play();

                state += 1;
                
                
                 //m_Material = GetComponent<Renderer>().material;

                
                break;
            }
            case 4: // 20 seconds of cooking and sound
            {
                if (timeRemaining < 20)
                {
                    timeRemaining += Time.deltaTime;

                    // make colour of chicken more dark over time
                    rend.material.color=Color.Lerp(chickenSkin, Color.black, (timeRemaining/20f));
                }
                else
                {
                    //oven.Stop();

                    // possibly play beeping noise

                    // quest complete
                    QuestManager.instance.SetQuestComplete("chickenCook");
                    oven.Stop();

                    state += 1;
                }
                break;
            }
            case 5:
            {
                break;
            }
            default:
            {
                break;
            }
        }
    }
}
