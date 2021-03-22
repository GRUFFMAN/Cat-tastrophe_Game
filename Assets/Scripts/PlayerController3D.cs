using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController3D : MonoBehaviour
{
    
    // Floats for stuffs
    public float gravity = -1f;
    public float speed = 1.0f;
    public float jumpForce = 5.0f;
    
    // WASD Axis 
    float walkZ;
    float walkX;
    
    // Rigidbody for Jump
    Rigidbody myRigidbody;

    // Bool for tracking if we are on the ground
    bool isGrounded;

    ///////////////////////////////////// Functions //////////////////////////////////////////

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

        Jump();
        
    }

    // Fixed update for movement
    void FixedUpdate() 
    {
        transform.position += (transform.forward * speed * walkZ) + (transform.right * speed * walkX);
    }

    // Handles jump, calls the grounded to check if we on ground
    void Jump()
    {
        Grounded();
        if(isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                myRigidbody.AddForce(Vector3.up * jumpForce);
            }
            if (Input.GetKey(KeyCode.Space))
            {
                // jump biggggg
            }
        }
        isGrounded = false;

    }
    
    // Called by jump to see if we are touching the ground
    void Grounded()
    {
        RaycastHit hit;
        Ray downRay = new Ray(transform.position, -Vector3.up);

        // Cast a ray straight downwards.
        if (Physics.Raycast(downRay, out hit))
        {
            if(hit.distance <= 0.51)
            {
                isGrounded = true;
            }
        }
    }
    
}
