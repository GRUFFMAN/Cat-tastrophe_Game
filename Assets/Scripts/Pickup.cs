﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public CharacterJoint springJoint;
	public GameObject heldItem;
	private Rigidbody heldRB;

	public GameObject pawOpen;
	public GameObject pawClosed;

	Color colourDebug = Color.red;
	public float maxDistance = 1f;

	public float maxHeldItemDist = 2.0f;
    bool itemGrabbed = false; //has an item been grabbed or not.
	Vector3 jointPos;

	private GameManager gameManager;


	float[] lookAngles = {0f, 0.01f, -0.01f};
    float[] upAngles = {0f, 0.01f, -0.01f};

	float lookDistance = 1.0f;

	void Start()
	{
		gameManager = (GameManager) GameObject.FindObjectOfType(typeof(GameManager));
		
		pawClosed = gameManager.pawClosed;
		pawOpen = GameObject.FindWithTag("PawOpen");

		pawClosed.SetActive(false);
	}

	void SetLayerRecursively(GameObject obj, int newLayer)
    {
        if (null == obj)
        {
            return;
        }
       
        obj.layer = newLayer;
       
        foreach (Transform child in obj.transform)
        {
            if (null == child)
            {
                continue;
            }
            SetLayerRecursively(child.gameObject, newLayer);
        }
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
				SetLayerRecursively(heldItem, 9);
				heldItem.layer = 9;
				heldItem = null;

				pawClosed.SetActive(false);
				pawOpen.SetActive(true);
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
									if (heldItem.layer == 9 || heldItem.layer == 16)
									{
										itemGrabbed = true;
										springJoint.connectedBody = heldRB;
										//Debug.Log("Pick up an Item");
										SetLayerRecursively(heldItem, 12);
										pawClosed.SetActive(true);
										pawOpen.SetActive(false);
									}
								}
							}
						}
					}
				}
			}		
		}
	}
}
