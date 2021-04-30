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
        float vel = Mathf.Round(rb.velocity.magnitude * 10f) / 10f;;
        Debug.Log(vel);
        if(vel > 1)
        {
            Debug.Log("YUH");
        }

        if(parent.name == "Dishes" && vel >= 0.5 && parent.layer == 9) // tired to add this layer exception so a dish couldn't break while you were carrying it. doesn't seem to have an effect
        {
            string type = body.name + "Broke";
            Debug.Log(type);
            GameObject broken = Instantiate(Resources.Load(type), col.transform.position, rb.rotation) as GameObject;
            broken.transform.parent = GameObject.Find("Dishes").transform;

            switch(broken.transform.childCount)
            {
                case 2:
                    GameObject child = broken.transform.GetChild(0).gameObject;
                    GameObject child2 = broken.transform.GetChild(1).gameObject;
                    child.transform.position = body.transform.position;
                    child2.transform.position = body.transform.position;
                    break;
                case 3:
                    GameObject child3 = broken.transform.GetChild(2).gameObject;
                    child3.transform.position = body.transform.position;
                    break;
                case 4:
                    GameObject child4 = broken.transform.GetChild(3).gameObject;
                    child4.transform.position = body.transform.position;
                    break;
            }
            Destroy(body);
        }
    }
}
