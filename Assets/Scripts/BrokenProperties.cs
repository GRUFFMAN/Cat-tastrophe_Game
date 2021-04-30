using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenProperties : BreakbleMaster
{
    [Header("Death RigidBodies")]
        [SerializeField] public Rigidbody brokenPart1;
        [SerializeField] public Rigidbody brokenPart2;
        [SerializeField] public Rigidbody brokenPart3;
        [SerializeField] public Rigidbody brokenPart4;
        [SerializeField] public Rigidbody brokenPart5;

    [Header("Force Values")]
        [SerializeField] public float fThrust = 1.0f;
        [SerializeField] public float rThrust = 1.0f;
        [SerializeField] public float uThrust = 1.0f;
    
    private Vector3 vectorforobject;

    //float lifeSpan = 4.0f;
    void Start()
    {
        vectorforobject = gameObject.transform.position;
        
        if(brokenPart1 != null)
        {
            brokenPart1.transform.position = vectorforobject;
            brokenPart1 = brokenPart1.GetComponent<Rigidbody>();
            brokenPart1.AddForce(transform.forward * fThrust);
            brokenPart1.AddForce(transform.right * rThrust);
            brokenPart1.AddForce(transform.up * uThrust);
        }
        if(brokenPart2 != null)
        {
            brokenPart2.transform.position = vectorforobject;
            brokenPart2 = brokenPart2.GetComponent<Rigidbody>();
            brokenPart2.AddForce(transform.forward * fThrust);
            brokenPart2.AddForce(transform.right * -rThrust);
            brokenPart2.AddForce(transform.up * uThrust);
        }
        if(brokenPart3 != null)
        {
            brokenPart3.transform.position = vectorforobject;
            brokenPart3 = brokenPart3.GetComponent<Rigidbody>();
            brokenPart3.AddForce(transform.forward * 100);
            brokenPart3.AddForce(transform.up * uThrust);

        }
        if(brokenPart4 != null)
        {
            brokenPart4.transform.position = vectorforobject;
            brokenPart4 = brokenPart4.GetComponent<Rigidbody>();
            brokenPart4.AddForce(transform.forward * 80);
            brokenPart4.AddForce(transform.right * (0.05f * -rThrust));
            brokenPart4.AddForce(transform.up * uThrust);
        }
        if(brokenPart5 != null)
        {
            brokenPart5.transform.position = vectorforobject;
            brokenPart4 = brokenPart5.GetComponent<Rigidbody>();
            brokenPart4.AddForce(transform.forward * 80);
            brokenPart4.AddForce(transform.right * (0.05f * rThrust));
            brokenPart4.AddForce(transform.up * uThrust);
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void DestroyGameObject() // destroy
    {
        Destroy(gameObject);
    }

    
}
