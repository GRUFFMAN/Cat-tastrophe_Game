using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Knockable : MonoBehaviour
{
    public GameObject cat;
    // Start is called before the first frame update
    public float hitForce = 1f;
    public float maxDistance = 1f;
    public float lastStep = 0.3f;         
    //float timeBetweenSteps = 0.3f;
    int forceDirection;
    Vector3 swipeDirection;
    private GameObject heldItem;

    public GameObject[] ventWaypoints;
    public GameObject[] ventPoints;

    int arrayVal;

    void Start()
    {
        /*ventWaypoints = GameObject.FindGameObjectsWithTag("Vent Waypoint");
        if (ventWaypoints  == null)
        {
            Debug.Log("ventWaypoints failed");
        }
        ventPoints = GameObject.FindGameObjectsWithTag("Ventpoint");
        if (ventPoints == null)
        {
            Debug.Log("Ventpoints failed");
        }
        */
    }

    // Update is called once per frame
     void Update()
    {
        if(Time.timeScale != 0f)
        { 
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                //Debug.Log("mouse click");
                RaycastHit hit;
                //forceDirection = Random.Range(1, 3);

                //if (Physics.Raycast(ray, out hit, maxDistance))
                if(Physics.Raycast(transform.position, transform.forward, out hit, maxDistance))
                {
                    if (hit.rigidbody != null && hit.transform.gameObject.layer != 6)
                    {                     
                        hit.rigidbody.velocity = Vector3.zero;
                        hit.rigidbody.AddForceAtPosition(transform.right * hitForce, hit.point);                    
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                //Debug.Log("mouse click");
                RaycastHit hit;
                //forceDirection = Random.Range(1, 3);

                //if (Physics.Raycast(ray, out hit, maxDistance))
                if(Physics.Raycast(transform.position, transform.forward, out hit, maxDistance))
                {
                    if (hit.rigidbody != null && hit.transform.gameObject.layer != 6)
                    {
                        hit.rigidbody.velocity = Vector3.zero;
                        hit.rigidbody.AddForceAtPosition(-transform.right * hitForce, hit.point);
                    }
                }
            }

            
            if(Input.GetKeyDown(KeyCode.E)) // code for venting. I made a few assumptions when making this code that I am glad work out.
                                            // My first assumption was that unity's findgtameobjectwithtag would add objects into the array in the order they were in the heirachy
                                            // I seem to have been lucky with the guess because my code works with 4 vents so far and no issue getting the correct pair.
                                            // Each vent has a pair, which is placed next to it in the heirachy in a 0,1 / 2,3 / 4,5 /6,7... etc fashion.
                                            // this coupled with my assumption of unity allows us to check if we are at a stop in the array and then add or subtract the index to find our pair and thus teleport to the correct spot.
                                            
                                            // Except now upon building the game the array is not being set up nice. so either my previous runs were lucky, or Unity compiles the scene differently than with the editor.
                                            // I will now just set up the arrays by hand.
            {
                RaycastHit ventHit;
                if(Physics.Raycast(transform.position, transform.forward, out ventHit, maxDistance)) // we start with a humble raycast and detect if we hit a vent
                {
                    if(ventHit.transform.gameObject.tag == "Ventpoint")
                    {
                        for(int i = 0; i < ventPoints.Length; i ++) // once we have we loop to see which array value this vent is at.
                        {
                            if(ventPoints[i].transform.position == ventHit.transform.position)
                            {   

                                if((i/2 == (i-1/2) || i == 1) && i != 0) // if the index is an odd, we subtract to find the pairs position. We also make sure that 0 is listed in the else.
                                {
                                    arrayVal = -1;
                                }
                                else // on positive we add to find the apir's position. 0 included.
                                {
                                    arrayVal = 1;
                                }
                                //Debug.Log(i + arrayVal);
                                cat.transform.position = ventWaypoints[(i+arrayVal)].transform.position; // once we have this position we can then easily teleport to the correct spot.

                            }
                        }
                    }
                }
            }
        
        
        
        }
    }





}
/*
                switch (forceDirection)
                {
                    case 1:
                        swipeDirection = transform.right;
                        break;
                    case 2:
                        swipeDirection = -transform.right;
                        break;
                }
*/
