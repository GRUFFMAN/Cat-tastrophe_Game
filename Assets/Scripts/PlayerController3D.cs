using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController3D : MonoBehaviour
{
    ///////////////////////////////////// Variables //////////////////////////////////////////
    // Floats for stuffs
    [Header("Controller Constants")]
        [SerializeField] public float gravity = -1f; // unused variable for custom gravity controls
        [SerializeField] public float speed = 1.0f;
        [SerializeField] public float jumpForce = 200.0f;
        [SerializeField] public float varJumpForce = 5f;
        [SerializeField] public float rayCastDistBelow = 0.59f;

    // parameters for acceleration curver on sprint
    [Header("Spint Curve Controls")]
        [SerializeField] float maxSpeed = 1f;
        [SerializeField] float MaxInitialAcceleration = 0.1f;
        [SerializeField] float sprintMulitlpier = 1.5f;
    
    // Audio Stuffs
    [Header("Audio Sounds Movement")]
        [SerializeField] public AudioClip my_LandSound;
        [SerializeField]public AudioClip my_JumpSound; 
        [SerializeField]public AudioClip my_FootStep; 
        [SerializeField]public AudioSource my_AudioSource;

    [Header("Audio Sounds Meow")]
        [SerializeField]public AudioClip meow1;
        [SerializeField]public AudioClip meow2;
        [SerializeField]public AudioClip meow3;
        [SerializeField]public AudioClip meow4;
        [SerializeField]public AudioClip loadedMeowSound;
    
    // WASD Axis 
    float walkZ;
    float walkX;
    
    // Rigidbody for Jump
    Rigidbody myRigidbody;

    // Bool for tracking if we are on the ground
    bool isGrounded;
    bool canVarJump = false;
    int sprintMode;
    Vector3 velocity;

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
        if(Input.GetKeyDown(KeyCode.E))
        {
            PlayMeowSounds();
        }

        
    }

    // Fixed update for movement
    void FixedUpdate() 
    {
        float acceleration = MaxInitialAcceleration * walkZ;
        float currentVelocity = myRigidbody.velocity.z;
        acceleration *= Mathf.Clamp(1.0f - (currentVelocity / maxSpeed), 0.0f, 1.0f);
        currentVelocity += acceleration;
        
        if(Input.GetKey(KeyCode.LeftShift)) // controls for sprint // currently a little broken for frame rate???
        {
            //transform.position += (transform.forward * acceleration * sprintMulitlpier) + (transform.right * speed * walkX);
            Vector3 velocity = ((transform.forward * acceleration * sprintMulitlpier) + ((transform.right * walkX) * speed)).normalized;
            velocity.y = myRigidbody.velocity.y;
            myRigidbody.velocity = velocity;
            
        }
        else
        {
            //transform.position += (transform.forward * walkZ * speed) + (transform.right * speed * walkX);
            Vector3 velocity = (((transform.forward * walkZ) * speed) + ((transform.right * walkX) * speed));
            velocity.y = myRigidbody.velocity.y;
            myRigidbody.velocity = velocity;

        }
        
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

    ///////////////////////////////////// Audio Functions //////////////////////////////////////////

    private void PlayJumpSound()
    {
        my_AudioSource.clip = my_JumpSound;
        my_AudioSource.Play();
    }

    private void PlayLandSound()
    {
        my_AudioSource.clip = my_LandSound;
        my_AudioSource.Play();
    }
    
    private void PlayMeowSounds()
    {
        int temp = Random.Range(1, 5);
        //Debug.Log(temp);
        switch (temp)
        {
            case 1:
                loadedMeowSound = meow1;
                break;
            case 2:
                loadedMeowSound = meow2;
                break;
            case 3:
                loadedMeowSound = meow3;
                break;
            case 4:
                loadedMeowSound = meow4;
                break;
        }   
        my_AudioSource.clip = loadedMeowSound;
        my_AudioSource.Play();
    }
    
    ///////////////////////////////////// Trigger Function ////////////////////////////////////////

    private void OnTriggerEnter(Collider other)
    {
        PlayLandSound();
    }

    private void OnTriggerStay(Collider other)
    {
        isGrounded = true;
    }
    
    private void OnTriggerExit(Collider other)
    {
        isGrounded = false;
    }

    ///////////////////////////////////// Jump Functions //////////////////////////////////////////

    // Handles jump, calls the grounded to check if we on ground
    void Jump()
    {
        //Grounded();
        if(isGrounded == true)
        {
            myRigidbody.velocity = Vector3.zero;
            myRigidbody.AddForce(Vector3.up * jumpForce);
            canVarJump = true;
            isGrounded = false;
            PlayJumpSound();
        }
    }
    void JumpVar()
    {
        if(canVarJump == true)
        {
            myRigidbody.AddForce(Vector3.up * (varJumpForce));
        }
    }
    
    
    ///////////////////////////////////// Grounded Function //////////////////////////////////////////
    
    // does nothing atm, using colliders to figure out if touching the ground.
    void Grounded()
    {

    }  
}

/*  // old grounded function using raycasts
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
*/



