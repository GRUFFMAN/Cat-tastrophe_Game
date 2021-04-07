using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProperties : BreakbleMaster
{
    public float lifeSpan = 5.0f; // lifespan of a bullet before it despawns
    public Vector3 location;

    // Start is called before the first frame update

    // Update is called once per frame
    void Start()
    {
        humanRef = GameObject.Find("Cat-Cher");
        location = transform.position;
        location.y = 1f;
        //humanRef.GetComponent<monsterBehaviour>().broken = 1;
        //humanRef.GetComponent<monsterBehaviour>().dmgLocation = location;
    }
    void Update()
    {
        Lifetime();
        
        

    }
     void DestroyGameObject() // destroy
    {
        Destroy(gameObject);
    }
    public void Lifetime()
    {
        lifeSpan -= (1*Time.deltaTime);  // lifespan gets counted down until 0 and then is destroyed
        if(lifeSpan <= 0)
        {
            DestroyGameObject();
        
        }
    }
    
}
