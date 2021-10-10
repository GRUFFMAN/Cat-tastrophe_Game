using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatSense : MonoBehaviour
{
    //List<Collider> questObjects = new List<Collider>();
    public GameObject[] questObjects;

    public float maxDistance = 6f;
    
    // Start is called before the first frame update
    void Start()
    {
        questObjects = GameObject.FindGameObjectsWithTag("QuestObject");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.O))
        {
            for(int i= 0; i < questObjects.Length; i++)
            {
                var outline = questObjects[i].gameObject.GetComponent<Outline>();
                if(Vector3.Distance(questObjects[i].transform.position, transform.position) <= maxDistance)
                {
                    outline.OutlineWidth = 4f;  
                }
                else
                {
                    outline.OutlineWidth = 0f;
                }
                
            }
        }
        else
        {
            for(int i= 0; i < questObjects.Length; i++)
            {
                var outline = questObjects[i].gameObject.GetComponent<Outline>();
                outline.OutlineWidth = 0f;    
                
            }
        }
    }
    
    
    
    
    /*
    void OnTriggerEnter(Collider col)
    {
        if(!questObjects.Contains(col) && (col.gameObject.tag == "QuestObject"))
        {
            questObjects.Add(col);
        }
    }
    void OnTriggerExit(Collider col)
    {
        if(questObjects.Contains(col))
        {
            questObjects.Remove(col);
        }
    }
    void FixedUpdate()
    {
        if(questObjects.Count > 0)
        {
            var outline = questObject.gameObject.GetComponent<Outline>();
            if(Input.GetKey(KeyCode.O))
            {
                outline.OutlineWidth = 4f;
                
            }
            else
            {
                outline.OutlineWidth = 0f;
                
            }
        }
    }
    */
}
