using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public CharacterJoint springJoint;
	private GameObject heldItem;
	private Rigidbody heldRB;

	Color colourDebug = Color.red;
	public float maxDistance = 1f;

	public float maxHeldItemDist = 2.0f;
    bool itemGrabbed = false; //has an item been grabbed or not.
	Vector3 jointPos;


	float[] lookAngles = {0f, 0.01f, -0.01f};
    float[] upAngles = {0f, 0.01f, -0.01f};

	float lookDistance = 1.0f;

	void Start()
	{

	}

	void Update()
	{
		if (Input.GetButtonDown("Fire1"))
        {
        	if(springJoint.connectedBody == null)
			{
				itemGrabbed = false;
				heldItem = null;
			}
			
			if(itemGrabbed == true)
			{
				springJoint.connectedBody = null;
				itemGrabbed = false;
				heldItem.layer = 9;
			}
		
			else
			{
				if (itemGrabbed == false)
				{
					RaycastHit hit;
					
					for(int z = 0; z < upAngles.Length; z++)
					{
						for(int i = 0; i < lookAngles.Length; i++)
						{
							//Debug.DrawRay(transform.position, (transform.forward + (transform.up * upAngles[z]) + (transform.right * lookAngles[i])).normalized * lookDistance, colourDebug);

							if(Physics.Raycast(transform.position, (transform.forward + (transform.up * upAngles[z]) + (transform.right * lookAngles[i])).normalized, out hit, lookDistance))
							{
								if (hit.transform != null)
								{
									heldItem = hit.transform.gameObject;
									heldRB = hit.rigidbody;
									if (heldItem.layer == 9)
									{
										itemGrabbed = true;
										springJoint.connectedBody = heldRB;
										Debug.Log("Pick up an Item");
										heldItem.layer = 12;
									}
								}
							}
						}
					}
				}
			}
			
		}
				
		
		/*
		if(itemGrabbed == true && springJoint.connectedBody != null)
		{
			Vector3 dist1 = heldItem.transform.position;
			Vector3 dist2 = springJoint.transform.position;
			if(Mathf.Abs(dist1.magnitude - dist2.magnitude) >= maxHeldItemDist )
			{
				springJoint.connectedBody = null;
				itemGrabbed = false;
				heldItem.layer = 9;
			}
		}	
		*/
	}
}
/*
void Update()
	{
		if (Input.GetButtonDown("Fire1"))
        {
        	if (itemGrabbed == false)
        	{
	            RaycastHit hit;
	            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

	            if (Physics.Raycast(ray, out hit, maxDistance))
	            {
	                if (hit.transform != null)
	                {
	                	heldItem = hit.transform.gameObject;
						heldRB = hit.rigidbody;
	                	if (heldItem.layer == 9)
	                	{
	                		itemGrabbed = true;
							springJoint.connectedBody = heldRB;
							Debug.Log("Pick up an Item");
							heldItem.layer = 12;

	                    }
	                }
	            }
	        }
		
			else
			{
				if(itemGrabbed == true)
				{
					springJoint.connectedBody = null;
					itemGrabbed = false;
					heldItem.layer = 9;
				}
			
			}
		}
		
		if(itemGrabbed == true && springJoint.connectedBody != null)
		{
			Vector3 dist1 = heldItem.transform.position;
			Vector3 dist2 = springJoint.transform.position;
			if(Mathf.Abs(dist1.magnitude - dist2.magnitude) >= maxHeldItemDist )
			{
				springJoint.connectedBody = null;
				itemGrabbed = false;
				heldItem.layer = 9;
			}
		}	
	}







public class Pickup : MonoBehaviour
{
    // Apply a force to a clicked rigidbody object.
	private GameObject heldItem;
	Rigidbody heldRigidbody; 
    // The force applied to an object when hit.
    public float grabSpeed = 100f;
	public float maxDistance = 1f;
    bool itemGrabbed = false; //has an item been grabbed or not.
    bool itemPulling = false; //is the item currently being pulled toward the player.

    void Start()
    {
    	
    }

    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
        	if (itemGrabbed == false)
        	{
	            RaycastHit hit;
	            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

	            if (Physics.Raycast(ray, out hit, maxDistance))
	            {
	                if (hit.transform != null)
	                {
	                	heldItem = hit.transform.gameObject;
	                	if (heldItem.layer == 9)
	                	{
	                		itemGrabbed = true;
	                		itemPulling = true;
	                    	heldItem.layer = 10;
	                    	heldRigidbody = hit.rigidbody;
	                    	heldRigidbody.velocity = Vector3.zero;
	                    	heldRigidbody.useGravity = false;
							heldItem.layer = 3;
	                    }
	                }
	            }
	        }
	        else
	        {
	        	Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

	        	if (itemPulling == true)
        		{
        			Vector3 goal = ray.GetPoint(1f);

        			Vector3 newPosition = Vector3.MoveTowards(heldItem.transform.position, goal, grabSpeed * Time.deltaTime);

        			heldItem.transform.SetPositionAndRotation(newPosition, heldItem.transform.rotation);
        			if (Vector3.Distance(goal, newPosition) == 0f)
        			{
        				itemPulling = false;
        			}
        		}
        		else
        		{

        				
						if (Input.GetKeyDown("left ctrl"))
        				{
        					heldRigidbody.rotation = Quaternion.Euler(0f, 0f, 0f);
        					heldRigidbody.angularVelocity = Vector3.zero;
        				}
						

        				heldRigidbody.MovePosition(ray.GetPoint(1f));
        			//heldItem.transform.SetPositionAndRotation(ray.GetPoint(2f), heldItem.transform.rotation);
        		}
	        }
	    }
        else
        {
        	if (itemGrabbed != false)
        	{
        		heldItem.layer = 9;
        		itemGrabbed = false;
        		heldRigidbody.useGravity = true;
        	}
        }
   	}
}
*/
