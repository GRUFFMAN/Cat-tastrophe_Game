using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Quests;

public class LOZscript : MonoBehaviour
{
    public GameObject cat;
    public GameObject lozPot;
    public GameObject rupee;

    public AudioSource catS;
    public AudioClip rupeeGet;

    public GameObject eInteract;

    private int state;

    // Update is called once per frame
    void Update()
    {
        switch(state)
        {
            case 0:
            {
                if(lozPot == null)
                {
                    rupee = GameObject.FindWithTag("rupee");
                    state += 1;
                }
                break;
            }
            case 1:
            {
                if(Vector3.Distance(cat.transform.position, rupee.transform.position) < 0.5f)
                {
                    eInteract.SetActive(true);
                    
                    if(Input.GetKeyDown(KeyCode.E))
                    {
                        eInteract.SetActive(false);
                        catS.clip = rupeeGet;
                        catS.Play();
                        
                        QuestManager.instance.SetQuestComplete("LOZrupee");
                        Destroy(rupee);
                        
                        state += 1;
                    }
                }
                break;
            }
            case 2:
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
