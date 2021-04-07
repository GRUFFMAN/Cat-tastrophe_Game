using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeOnCrash : BreakbleMaster
{
    public Rigidbody deadModel;
    public float breakPoint;
    Vector3 v;
    public float acceleration;
    public float distancemoved=0;
    public float lastdistancemoved=0;
    public Vector3 last;
    public Rigidbody rb;
    public AudioClip m_LandSound; 
    public AudioSource m_AudioSource;
    void Start()
    {
        last = transform.position;
        rb = gameObject.GetComponent<Rigidbody>();

    }
    void Update()
    {   
        acceleration = (Mathf.Sqrt(Mathf.Pow(rb.velocity.x, 2) + Mathf.Pow(rb.velocity.y, 2) + Mathf.Pow(rb.velocity.z, 2))) ;
        //Debug.Log(rb.velocity);

        v = transform.position; //Track the object
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "break")
        {
            CheckExplode();
            //Debug.Log("I should explode");
        }
    }

    void Explode()
    {
        Rigidbody newBullet = Instantiate(deadModel, v, deadModel.rotation) as Rigidbody; //spawn the dead
    }

    void DamageTracker()
    {

    }

    void CheckExplode()
    {
        if(gameObject.layer == 0)
        {
            if(acceleration > breakPoint)
            {
                Destroy(gameObject);
                PlayLandingSound();
                Explode();
            }
        }

        Debug.Log(acceleration);
        //Debug.Log(gameObject.layer);
    }
    private void PlayLandingSound()
    {
        m_AudioSource.clip = m_LandSound;
        m_AudioSource.Play();
    }
}
