using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatCatching : MonoBehaviour
{
    public bool isCaught = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == "Player")
        {
            isCaught = true;


        }
    }
}
