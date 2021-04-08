﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        				/*
						if (Input.GetKeyDown("left ctrl"))
        				{
        					heldRigidbody.rotation = Quaternion.Euler(0f, 0f, 0f);
        					heldRigidbody.angularVelocity = Vector3.zero;
        				}
						*/

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