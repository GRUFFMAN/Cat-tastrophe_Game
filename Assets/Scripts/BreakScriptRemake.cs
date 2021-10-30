using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakScriptRemake : MonoBehaviour
{
    public float breakPoint = 3.0f;
    Vector3 v;
    Rigidbody rb;
    float acceleration;
    Vector3 last;
    public int score = 100;

    bool canExplode = true;
    CollisionDetectionMode collisionDetectionMode;

    AudioSource self;
    AudioClip soundToLoad;
    
    // Start is called before the first frame update
    void Start()
    {
        last = transform.position;
        rb = gameObject.GetComponent<Rigidbody>();
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;

    }
    void Update()
    {   
        acceleration = (Mathf.Sqrt(Mathf.Pow(rb.velocity.x, 2) + Mathf.Pow(rb.velocity.y, 2) + Mathf.Pow(rb.velocity.z, 2)));
        //Debug.Log(rb.velocity);
        v = transform.position; //Track the object
    }
    
    void OnCollisionEnter(Collision col)
    {
        //Debug.Log(acceleration);
        if(gameObject.layer == 9 && canExplode == true)
        {
            if(acceleration > breakPoint)
            {
                Explode();
                //Debug.Log("I should explode");
                canExplode = false;
            }
        }
    }
    
    void Explode()
    {
        string type = gameObject.name + "Broke";
            //Debug.Log(type);
            GameObject broken = Instantiate(Resources.Load(type), gameObject.transform.position, rb.rotation) as GameObject;

            self = broken.GetComponent<AudioSource>();
            if (broken.tag == "Glass")
            {
                soundToLoad = Resources.Load<AudioClip>("GlassBreakSound");
            } 
            
            else if (broken.tag == "Other")
            {
                soundToLoad = Resources.Load<AudioClip>("OtherBreakSound");               
            }
            
            self.clip = soundToLoad;
            self.Play();

            //Switch based on number of child objects sets their position
            switch(broken.transform.childCount)
            {
                case 2:
                    GameObject child = broken.transform.GetChild(0).gameObject;
                    GameObject child2 = broken.transform.GetChild(1).gameObject;
                    child.transform.position = gameObject.transform.position;
                    child.layer = 9;
                    child2.transform.position = gameObject.transform.position;
                    child2.layer = 9;
                    break;
                case 3:
                    GameObject child3 = broken.transform.GetChild(2).gameObject;
                    child3.transform.position = gameObject.transform.position;
                    child3.layer = 9;
                    break;
                case 4:
                    GameObject child4 = broken.transform.GetChild(3).gameObject;
                    child4.transform.position = gameObject.transform.position;
                    child4.layer = 9;
                    break;
                case 5:
                    GameObject child5 = broken.transform.GetChild(4).gameObject;
                    child5.transform.position = gameObject.transform.position;
                    child5.layer = 9;
                    break;
                case 6:
                    GameObject child6 = broken.transform.GetChild(5).gameObject;
                    child6.transform.position = gameObject.transform.position;
                    child6.layer = 9;
                    break;
                case 7:
                    GameObject child7 = broken.transform.GetChild(6).gameObject;
                    child7.transform.position = gameObject.transform.position;
                    child7.layer = 9;
                    break;
            }
            GameObject gameManager = GameObject.Find("gameManager");
            gameManager.GetComponent<GameManager>().currentScore += score;
            Destroy(gameObject);
    }
}
