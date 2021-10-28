using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Quests;

public class ToothBrush : MonoBehaviour
{
    public GameObject toothBrush;
    bool done = false;

    void OnTriggerEnter(Collider col)
    {
        if(done != true)
        {
            if(col.gameObject.name == "toothbrush")
            {
                //Debug.Log("complete");
                QuestManager.instance.SetQuestComplete("ToothBrush");
                done = true;
            }
        }
        
    }
}
