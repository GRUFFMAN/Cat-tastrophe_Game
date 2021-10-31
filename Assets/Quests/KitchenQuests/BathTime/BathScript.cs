using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Quests;

public class BathScript : MonoBehaviour
{
    private int state = 0;

    public GameObject cat;
    public GameObject tap;
    public GameObject soap;
    public GameObject particle;
    AudioSource self;
    public AudioClip water;

    float timeRemaining;
    bool inBath = false;
    
    
    // Start is called before the first frame update
    void Start()
    {
        self = GetComponent<AudioSource>();
        self.clip = water;
    }

    // Update is called once per frame
    void Update()
    {        
        switch(state)
        {
            case 0: // await and count e
            {  
                if(Vector3.Distance(soap.transform.position, tap.transform.position) < 0.3f)
                {
                    timeRemaining = 0f;
                    self.Play();
                    
                    state += 1;
                    

                    Debug.Log("soap in the sink");
                }
                break;
            }
            case 1: // tap turns on, particle play, water rises
            {
                if (timeRemaining < 5f)
                {
                    timeRemaining += Time.deltaTime;

                    // activate particles
                    particle.SetActive(true);

                    transform.position += 0.03f * transform.up * Time.deltaTime;
                }
                else
                {
                    // stop particles if needed 
                    Debug.Log("Water at max level");
                    particle.SetActive(false);    
                    self.Stop();              

                    state += 1;
                }
                
                break;
            }
            case 2: // if player enters bath
            {
                if(inBath)
                {
                    // complete quest
                    QuestManager.instance.SetQuestComplete("bathWater");
                    state +=1;

                    Debug.Log("Quest complete");
                }
                break;
            }
            case 3:
            {
                break;
            }
            default:
            {
                break;
            }
        }
    }

    void OnTriggerStay(Collider col)
    {
        if(col.tag == "Player")
        {
            inBath = true;
        }
    }
}
