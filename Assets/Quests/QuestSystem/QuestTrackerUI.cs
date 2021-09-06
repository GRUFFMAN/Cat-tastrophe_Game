using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Quests
{
    public interface ITrackedQuestUIItem
    {
        public void Init(QuestTrackerUI trackerUI, QuestDetails questdetails, int questSubLevel = 0); //init the ui with quest details
        public void Refresh(QuestStatus status); //refresh the ui state
    }

    public class TrackedQuestUIItem : MonoBehaviour, ITrackedQuestUIItem
    {
        QuestTrackerUI trackerUI;
        QuestDetails questDetails;

        public virtual void Init(QuestTrackerUI trackerUI, QuestDetails questdetails, int questSubLevel = 0)
        {
            this.trackerUI = trackerUI;
            this.questDetails = questdetails; 
        }
        public virtual void Refresh(QuestStatus status)
        {
            //do whatever you wish here.
        }

    }

    public class QuestTrackerUI : MonoBehaviour
    {
        public GameObject questsPanelRoot;
        QuestManager qm;
        public TrackedQuestUIItem uiItemPrefab;
        Dictionary<string, TrackedQuestUIItem> trackedQuests;
        
        // Start is called before the first frame update
        void Start()
        {
            qm = QuestManager.instance;
            trackedQuests = new Dictionary<string, TrackedQuestUIItem>();
            CreateQuestAndChildren(qm.GetQuests(), null);    
        }

        void CreateQuestAndChildren(QuestDetails[] qd, QuestDetails parent = null, int level = -1)
        {
            level += 1;
            foreach (QuestDetails q in qd)
            {
                if (q.parentQuest == parent)
                {
                    TrackedQuestUIItem tqui = Instantiate(uiItemPrefab, Vector3.zero, Quaternion.identity);
                    tqui.Init(this, q, level);
                    tqui.transform.SetParent(questsPanelRoot.transform);
                    trackedQuests[q.questID] = tqui;

                    //create children
                    CreateQuestAndChildren(qd, q, level);
                }
            }
        }

        public void OnQuestChangeStatus(string questID, QuestStatus newstatus)
        {
            if (trackedQuests.ContainsKey(questID))
            {
                trackedQuests[questID].Refresh(newstatus);
            }
        }
    }
}