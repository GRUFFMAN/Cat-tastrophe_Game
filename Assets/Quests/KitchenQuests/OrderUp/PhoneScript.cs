using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Quests;

public class PhoneScript : MonoBehaviour
{
    // phone waits until the player knocks it a few times/ or interacts with "e"

    // after a few interaction the phone emits a dial tone, and a person picks up awaiting a pizza order.

    // after a meow dialogue progresses, and pizza is arranged for delivery.

    // pizza arrives at the door super fast, doorbell sound.

    // player eats slice of pizza. Complete quest.

    private int state = 0;
    public GameObject cat;
    public GameObject phoneRec;
    public GameObject pizzaFood;
    public AudioSource phone;
    public AudioSource door;
    public AudioClip dial;
    public AudioClip hangUp;
    public AudioClip doorBell;


    // UI stuffs
    public GameObject takeOrder;
    public GameObject peperoni;
    public GameObject location;
    public GameObject soon;
    public GameObject eInteract;

    float timeRemaining;

    void Update()
    {
        switch(state)
        {
            case 0:
            case 1:
            case 2: // await and count e's // 3 e's
            {
                
                if(Vector3.Distance(cat.transform.position, transform.position) < 0.5f)
                {
                    eInteract.SetActive(true);
                    
                    if(Input.GetKeyDown(KeyCode.E))
                    {
                        eInteract.SetActive(false);
                        Debug.Log(state);
                        // play dial beep
                        state += 1;
                    }
                }
                else
                {
                    eInteract.SetActive(false);
                }
                break;
            }
            case 3: // dial tone, how may I take you order.
            {
                // play dial tone
                phone.clip = dial;
                phone.Play();
                state += 1;
                timeRemaining = 12f;
                QuestManager.instance.SetQuestComplete("pizzaDial");
                break;
            }
            case 4: // awaits meow
            {
                if (timeRemaining > 0)
                {
                    timeRemaining -= Time.deltaTime;
                }
                else
                {
                    phone.Stop();
                    takeOrder.SetActive(true);
                    
                    if(Input.GetKeyDown(KeyCode.M))
                    {
                        if(Vector3.Distance(cat.transform.position, phoneRec.transform.position) < 0.5f)
                        {
                            takeOrder.SetActive(false);
                            state +=1;
                        }
                    }
                }
                break;
            }
            case 5: // 1 peporoni pizza?
            {
                peperoni.SetActive(true);
                
                if(Input.GetKeyDown(KeyCode.M))
                {
                    if(Vector3.Distance(cat.transform.position, phoneRec.transform.position) < 0.5f)
                    {
                        peperoni.SetActive(false);
                        state +=2;
                    }
                }
                
                break;
            }
            case 7: // address please?
            {
                location.SetActive(true);
                
                if(Input.GetKeyDown(KeyCode.M))
                {
                    if(Vector3.Distance(cat.transform.position, phoneRec.transform.position) < 0.5f)
                    {
                        location.SetActive(false);
                        state +=2;
                        timeRemaining = 3f;
                    }
                }

                break;
            }
            case 9: // hangup noise
            {
                
                if (timeRemaining > 0)
                {
                    timeRemaining -= Time.deltaTime;
                    soon.SetActive(true);
                }
                else
                {
                    soon.SetActive(false);
                    QuestManager.instance.SetQuestComplete("pizzaOrder");
                    state += 1;
                }
                
                break;
            }
            case 10: // ding dong noise, pizza at door
            {
                door.clip = doorBell;
                door.Play();

                pizzaFood = Instantiate(pizzaFood, door.gameObject.transform.position, Quaternion.identity);

                state += 1;
                break;
            }
            case 11: // eat pizza, complete quest
            {
                
                
                if(Vector3.Distance(cat.transform.position, door.gameObject.transform.position) < 1f)
                {
                    eInteract.SetActive(true);
                    if(Input.GetKeyDown(KeyCode.E))
                    {
                        eInteract.SetActive(false);
                        QuestManager.instance.SetQuestComplete("pizzaEat");
                        state += 1;
                    }
                }
                else
                {
                    eInteract.SetActive(false);
                }

                
                break;
            }
            case 12:
            {
                break;
            }
            default:
            {
                Debug.Log("Pizza Quest error");
                break;
            }
        }
    }
}
