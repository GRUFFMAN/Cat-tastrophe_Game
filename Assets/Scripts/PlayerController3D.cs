using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController3D : MonoBehaviour
{
    ///////////////////////////////////// Variables //////////////////////////////////////////
    // Floats for stuffs
    public float gravity = -1f;
    public float speed = 1.0f;
    public float jumpForce = 200.0f;
    public float varJumpForce = 5f;
    public float rayCastDistBelow = 0.59f;
    
    // WASD Axis 
    float walkZ;
    float walkX;
    
    // Rigidbody for Jump
    Rigidbody myRigidbody;

    // Bool for tracking if we are on the ground
    bool isGrounded;
    public bool canVarJump = false;

    ///////////////////////////////////// Standard Functions //////////////////////////////////////////

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        walkZ = Input.GetAxis("Vertical");
        walkX = Input.GetAxis("Horizontal");

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        
        
    }

    // Fixed update for movement
    void FixedUpdate() 
    {
        transform.position += (transform.forward * speed * walkZ) + (transform.right * speed * walkX);
        
        if(canVarJump == true)
        {
            if(Input.GetKey(KeyCode.Space))
            {
                JumpVar();
            }
            else
            {
                canVarJump = false;
            }
        }
    }

    ///////////////////////////////////// Jump Functions //////////////////////////////////////////

    // Handles jump, calls the grounded to check if we on ground
    void Jump()
    {
        Grounded();
        if(isGrounded == true)
        {
            myRigidbody.velocity = Vector3.zero;
            myRigidbody.AddForce(Vector3.up * jumpForce);
            canVarJump = true;
            isGrounded = false;
        }
    }
    void JumpVar()
    {
        //Grounded();
        if(canVarJump == true)
        {
            myRigidbody.AddForce(Vector3.up * (varJumpForce));
        }
    }
    
    
    ///////////////////////////////////// Grounded Function //////////////////////////////////////////

    // Called by jump to see if we are touching the ground
    void Grounded()
    {
        
        RaycastHit hit;
        GameObject hitObj;
        float offset1 = 0.50f;
        float offset2 = 0.35f;
        // Define 5 rays at various points under the player model to determine if they are grounded
        Ray downRay = new Ray(myRigidbody.transform.position, -Vector3.up);
        Ray bdownRay = new Ray(transform.position + transform.forward * offset1, -Vector3.up);
        Ray fdownRay = new Ray(transform.position + -transform.forward * offset1, -Vector3.up);
        Ray rdownRay = new Ray(transform.position + transform.right * offset2, -Vector3.up);
        Ray ldownRay = new Ray(transform.position + -transform.right * offset2, -Vector3.up);
        

        // Cast a ray straight downwards.
        if(Physics.Raycast(downRay, out hit))
        {
            hitObj = hit.transform.gameObject;
            if(hitObj.layer != 6 && hit.distance <= rayCastDistBelow)
            {
                isGrounded = true;
                return;
            }
        }

        // Cast the front and back ground checks
        if(Physics.Raycast(fdownRay, out hit))
        {
            hitObj = hit.transform.gameObject;
            if(hitObj.layer != 6 && hit.distance <= rayCastDistBelow)
            {
                isGrounded = true;
                return;
            }
        }
        if(Physics.Raycast(bdownRay, out hit))
        {
            hitObj = hit.transform.gameObject;
            if(hitObj.layer != 6 && hit.distance <= rayCastDistBelow)
            {
                isGrounded = true;
                return;
            }
        }

        // Cast the right and left ground checks
        if(Physics.Raycast(rdownRay, out hit))
        {
            hitObj = hit.transform.gameObject;
            if(hitObj.layer != 6 && hit.distance <= rayCastDistBelow)
            {
                isGrounded = true;
                return;
            }
        }
        if(Physics.Raycast(ldownRay, out hit))
        {
            hitObj = hit.transform.gameObject;
            if(hitObj.layer != 6 && hit.distance <= rayCastDistBelow)
            {
                isGrounded = true;
                return;
            }
        }

        // if nothing hit, ground is false
        else
        {
            isGrounded = false;
        }
    }
    
}

// old useless code for ref
/*
        if(Mathf.Abs(myRigidbody.velocity.y) <= 0f) // "at rest" being a little liberal, if we waited for zero you would basically need to be stationary too, trying to get soemthing that is comfortable as a controller.
        {
            isGrounded = true;
            return;
            // return if at rest, don't bother with the raycasts
        }
    */



