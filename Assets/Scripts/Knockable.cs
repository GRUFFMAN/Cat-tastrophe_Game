using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Knockable : MonoBehaviour
{
    // Start is called before the first frame update
    public float hitForce = 1f;
    public float maxDistance = 1f;
    public float lastStep = 0.3f;         //used for controlling the firerate of the guns
    float timeBetweenSteps = 0.3f;
    int forceDirection;
    Vector3 swipeDirection;
    private GameObject heldItem;

    void Start()
    {
        
    }

    // Update is called once per frame
    public void Update()
    {
            
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //forceDirection = Random.Range(1, 3);

            if (Physics.Raycast(ray, out hit, maxDistance))
            {
                if (hit.rigidbody != null)
                {                     
                    hit.rigidbody.velocity = Vector3.zero;
                    hit.rigidbody.AddForceAtPosition(transform.right * hitForce, hit.point);                    
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //forceDirection = Random.Range(1, 3);

            if (Physics.Raycast(ray, out hit, maxDistance))
            {
                if (hit.rigidbody != null)
                {
                    hit.rigidbody.velocity = Vector3.zero;
                    hit.rigidbody.AddForceAtPosition(-transform.right * hitForce, hit.point);
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

                        
                        
            if(heldItem != null)
        {
            if(Time.time - lastStep > timeBetweenSteps) //controls the firerate of the guns so they aren't firing evey update
            {
                heldItem.layer = 9;
            }
        }
*/
