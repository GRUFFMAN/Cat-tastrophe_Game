using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableManager : MonoBehaviour
{
    public int cableLength = 10;
    public GameObject loadedCableSection;
    float steplength;
    Vector3 space;

    void Start()
    {
        space = transform.position;
        steplength = loadedCableSection.transform.localScale.y;

        for(int i = 0; i <= cableLength; i++)
        {
            space.z = (space.z + i*steplength);
            GameObject section = Instantiate(loadedCableSection, space, Quaternion.identity) as GameObject;
        }
        
        
        
        //GameObject[] cableSections;
        //Component[] joints;
        

        
        

        // First read the cable length and use it to spawn x amount of cable sections using prefab base.

        // Second geat each components joint connection and set it to the next section along

        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
