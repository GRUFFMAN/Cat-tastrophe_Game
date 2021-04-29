using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breakScript : MonoBehaviour
{
    public void OnCollisionEnter(Collision col)
    {
        GameObject body = col.gameObject;
        Rigidbody rb = body.GetComponent<Rigidbody>();
        GameObject parent = body.transform.parent.gameObject;
        float vel = rb.velocity.magnitude;
        Debug.Log(parent.name);
        if(parent.name == "Dishes" && vel > 0.6f)
        {
            //GameObject ass = Instantiate(Resources.Load("GlassBroke"), Quaternion.identity as GameObject);
            //body.Destroy(body);
        }
    }
}
