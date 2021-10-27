using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Quests;

public class CakeScript : MonoBehaviour
{
    // Obj References
    public GameObject bowl1, chinabowl, milk, sugar, flour, particle;

    public GameObject cat, spinner;

    public GameObject eInteract;

    private float time = 0;

    private int state = 0;


    void Start()
    {

    }

    void Update()
    {
        switch(state)
        {
            // I want to recognise that the cat has certain objects that it needs to bring to the mixer
            // I need a reference to the cat to check if it is close and if it has the needed objectin it's mouth
            // once it is brought to the mixer thy are promted to drop it into the mixer and it is destroyed/hidden and the appropraite mixer model is unhidden.
            // once all needed items are brought to the mixer the player will be prompted to turn on the mixer spraying the particle effect everywhere.
            // end quest.

            case 0: // first step should be to await the mixing bowl choice.
                    // I want the player to be free to choose any bowl, since they can all break. so either a Large_Bowl or a ChinaBowl
                    // we cant add any of the other ingredients without a bowl of course.
            {
                if(Vector3.Distance(cat.transform.position, transform.position) < 0.75f)
                {
                    if(cat.GetComponent<Pickup>().heldItem != null)
                    {
                        if(cat.GetComponent<Pickup>().heldItem.name == "Low_Poly_Large_Bowl")
                        {
                            eInteract.SetActive(true);
                            if(Input.GetKeyDown(KeyCode.E))
                            {
                                bowl1.SetActive(true);
                                eInteract.SetActive(false);
                                cat.GetComponent<Pickup>().heldItem.SetActive(false);
                                cat.GetComponent<Pickup>().heldItem = null;
                                cat.GetComponent<Pickup>().pawClosed.SetActive(false);
                                cat.GetComponent<Pickup>().pawOpen.SetActive(true);
                                cat.GetComponent<Pickup>().springJoint.connectedBody = null;

                                state += 1;
                            }
                        }
                        else if(cat.GetComponent<Pickup>().heldItem.name == "ChinaBowl")
                        {
                            eInteract.SetActive(true);
                            if(Input.GetKeyDown(KeyCode.E))
                            {
                                chinabowl.SetActive(true);
                                eInteract.SetActive(false);
                                cat.GetComponent<Pickup>().heldItem.SetActive(false);
                                cat.GetComponent<Pickup>().heldItem = null;
                                cat.GetComponent<Pickup>().pawClosed.SetActive(false);
                                cat.GetComponent<Pickup>().pawOpen.SetActive(true);
                                cat.GetComponent<Pickup>().springJoint.connectedBody = null;

                                state += 1;
                            }
                        }
                    }
                    else
                    {
                        eInteract.SetActive(false);
                    }
                }
                else
                {
                    eInteract.SetActive(false);
                }

                break;
            }
            case 1: 
            {
                if(Vector3.Distance(cat.transform.position, transform.position) < 0.75f)
                {
                    if(cat.GetComponent<Pickup>().heldItem != null)
                    {
                        if(cat.GetComponent<Pickup>().heldItem.name == "Sugar")
                        {
                            eInteract.SetActive(true);
                            if(Input.GetKeyDown(KeyCode.E))
                            {
                                sugar.SetActive(true);
                                eInteract.SetActive(false);
                                cat.GetComponent<Pickup>().heldItem.SetActive(false);
                                cat.GetComponent<Pickup>().heldItem = null;
                                cat.GetComponent<Pickup>().pawClosed.SetActive(false);
                                cat.GetComponent<Pickup>().pawOpen.SetActive(true);
                                cat.GetComponent<Pickup>().springJoint.connectedBody = null;
                            }
                        }
                        else if(cat.GetComponent<Pickup>().heldItem.name == "Flour")
                        {
                            eInteract.SetActive(true);
                            if(Input.GetKeyDown(KeyCode.E))
                            {
                                flour.SetActive(true);
                                eInteract.SetActive(false);
                                cat.GetComponent<Pickup>().heldItem.SetActive(false);
                                cat.GetComponent<Pickup>().heldItem = null;
                                cat.GetComponent<Pickup>().pawClosed.SetActive(false);
                                cat.GetComponent<Pickup>().pawOpen.SetActive(true);
                                cat.GetComponent<Pickup>().springJoint.connectedBody = null;
                            }
                        }
                        else if(cat.GetComponent<Pickup>().heldItem.name == "Milk")
                        {
                            eInteract.SetActive(true);
                            if(Input.GetKeyDown(KeyCode.E))
                            {
                                milk.SetActive(true);
                                eInteract.SetActive(false);
                                cat.GetComponent<Pickup>().heldItem.SetActive(false);
                                cat.GetComponent<Pickup>().heldItem = null;
                                cat.GetComponent<Pickup>().pawClosed.SetActive(false);
                                cat.GetComponent<Pickup>().pawOpen.SetActive(true);
                                cat.GetComponent<Pickup>().springJoint.connectedBody = null;
                            }
                        }
                        if(sugar.activeSelf == true && flour.activeSelf == true && milk.activeSelf == true)
                        {
                            state += 1;
                        }
                    }
                    else
                    {
                        eInteract.SetActive(false);
                    }
                }
                else
                {
                    eInteract.SetActive(false);
                }
                break;
            }
            case 2:       
            {
                particle.SetActive(true);
                time = 5f;
                state += 1;
                break;
            }
            case 3: 
            {
                if(time > 0)
                {
                    spinner.transform.Rotate(0f, 700f, 0f, Space.Self);
                    time -= Time.deltaTime;
                }
                else
                {
                    QuestManager.instance.SetQuestComplete("CakeIsALie");
                    state += 1; 

                }
                
                break;
            }
            case 4: 
            {
                // end
                break;
            }
            default:
            {
                break;
            }
        }
    }
}
