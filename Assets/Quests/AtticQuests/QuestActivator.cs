using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Quests
{
    public class QuestActivator : MonoBehaviour
    {
        public GameObject exit;
        bool foundExit = false;
        public GameObject w;
        public GameObject a;
        public GameObject d;
        public GameObject s;
        public GameObject wasdHolder;
        public GameObject m;
        public GameObject q;
        public GameObject destroyHolder;
        public GameObject eToLeave;
        //QuestManager qm;

        void Start()
        {
            
        }
        
        // Update is called once per frame
        void Update()
        { 
            //QuestManager.instance.SetQuestComplete("questid");
            if(Input.GetKeyDown(KeyCode.W))
            {
                QuestManager.instance.SetQuestComplete("W");
                w.SetActive(false);
            }
            if(Input.GetKeyDown(KeyCode.A))
            {
                QuestManager.instance.SetQuestComplete("A");
                a.SetActive(false);
            }
            if(Input.GetKeyDown(KeyCode.S))
            {
                QuestManager.instance.SetQuestComplete("S");
                s.SetActive(false);
            }
            if(Input.GetKeyDown(KeyCode.D))
            {
                QuestManager.instance.SetQuestComplete("D");
                d.SetActive(false);
            }
            if(QuestManager.instance.WasParentQuestCompleted(QuestManager.instance.quests["W"]))
            {
                wasdHolder.SetActive(false);
                destroyHolder.SetActive(true);
            }
            

            if(Input.GetKeyDown(KeyCode.Q))
            {
                QuestManager.instance.SetQuestComplete("atticQPickup");
                q.SetActive(false);
            }

            if(Input.GetMouseButtonDown(0))
            {
                QuestManager.instance.SetQuestComplete("atticMouse");
                m.SetActive(false);
            }
            if(Input.GetMouseButtonDown(1))
            {
                QuestManager.instance.SetQuestComplete("atticMouse");
                m.SetActive(false);
            }
            if(QuestManager.instance.WasParentQuestCompleted(QuestManager.instance.quests["atticQPickup"]))
            {
                destroyHolder.SetActive(false);
            }

            // tell if escaped
            if(foundExit == true)
            {
                if(Input.GetKeyDown(KeyCode.E))
                {   
                    QuestManager.instance.SetQuestComplete("atticExit");
                    eToLeave.SetActive(false);
                }
            }

        }

        void OnTriggerEnter(Collider col)
        {
            if(col.tag == "Exit")
            {
                foundExit = true;
                Debug.Log("Player Found Exit");
                
                eToLeave.SetActive(true);
                
            }
        }
        void OnTriggerExit(Collider col)
        {
            if(col.tag == "Exit")
            {
                foundExit = false;
                Debug.Log("Player Left the Exit");
                
                eToLeave.SetActive(false);
                
            }
        }

    }
}