using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Quests
{
    public class QuestManager : MonoBehaviour
    {
        //This structure is purely so designers can enter quest details
        //in the inspector
        public QuestDetails[] inspectorQuests;
        //This structure we will fill in from the inspectorQuests
        //array - it will be easier to work with because it is keyed
        //on the ID, so easier to find quests quickly.
        public Dictionary<string, QuestDetails> quests;
        private bool inited = false;
        static QuestManager _instance;

        //delegate void QuestCompleteCallback(string questID);
        public UnityEvent<string, QuestStatus> onQuestChangeStatus;
        public UnityEvent onAllQuestsComplete;

        //Only allow one quest manager to exist in the scene
        //and persist it between scenes.
        public void Awake()
        {
            if (_instance == null)
            {
                EnsureInited();
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public static QuestManager instance
        {
            get { return _instance; }
        }

        public void EnsureInited()
        {
            if (!inited)
            {
                inited = true;
                quests = new Dictionary<string, QuestDetails>();
                foreach (QuestDetails quest in inspectorQuests)
                {
                    quest.status = QuestStatus.NOT_STARTED;
                    quests[quest.questID] = quest;
                }
                Debug.Log("Quest System Inited");
            }
        }

        public QuestDetails[] GetQuests()
        {
            QuestDetails[] details = new QuestDetails[quests.Count];
            quests.Values.CopyTo(details, 0);
            return details;        
        }
        public QuestStatus GetQuestStatus(string questID)
        {
            QuestStatus retval = QuestStatus.NO_STATUS;
            if (quests.ContainsKey(questID))
            {
                retval = quests[questID].status;
            }
            return retval;
        }

        public QuestDetails[] GetSubQuests(string questid)
        {
            List<QuestDetails> subquests = new List<QuestDetails>();
            if (quests.ContainsKey(questid))
            {
                QuestDetails parentQuest = quests[questid];
                foreach (QuestDetails quest in quests.Values)
                {
                    if (quest.parentQuest == parentQuest)
                    {
                        subquests.Add(quest);
                    }
                }
            }
            return subquests.ToArray();
        }

        public bool WasParentQuestCompleted(QuestDetails somequest)
        {
            bool retval = false;
            QuestDetails parentquest = somequest.parentQuest;
            if (parentquest != null)
            {
                QuestDetails[] subquests = GetSubQuests(parentquest.questID);
                bool allSubquestsCompleted = true;
                foreach (QuestDetails subquest in subquests)
                {
                    if (subquest.status != QuestStatus.COMPLETE)
                    {
                        allSubquestsCompleted = false;
                        break;
                    }
                }
                if (allSubquestsCompleted)
                {
                    SetQuestComplete(parentquest.questID);
                    retval = allSubquestsCompleted;
                }
            }
            return retval;
        }

        //returns true if the quest changed status.
        //Invokes onQuestChangedStatus if this is the case.
        //Invokes onAllQuestsComplete if all quests are complete
        public bool SetQuestStatus(string questID, QuestStatus status)
        {
            bool retval = false;
            if (quests.ContainsKey(questID) && quests[questID].status != status)
            {
                quests[questID].status = status;
                onQuestChangeStatus.Invoke(questID, status);
                retval = true;

                //Initially I thought I'd need to recursively check
                //completed parent quests here, but it will be handled
                //when the below function does a SetQuestComplete on the
                //parent quest, if it exists.
                WasParentQuestCompleted(quests[questID]);


                //If the check was complete, lets see if it:
                //-happens to tick off a parent quest
                //-happens to complete all quests
                //This is pretty inefficient, we exhaustively check everything
                //but it's only triggered when quests are completed - so
                //it's not a big deal.
                if (status == QuestStatus.COMPLETE) 
                {
                    //Now check for all complete
                    bool allComplete = true;
                    foreach (QuestDetails quest in quests.Values)
                    {
                        if (quest.status != QuestStatus.COMPLETE)
                        {
                            allComplete = false;
                            break;
                        }
                    }
                    if (allComplete)
                    {
                        onAllQuestsComplete.Invoke();
                    }
                }
            }
            return retval;
        }

        //Convenience method for marking a quest complete
        public bool SetQuestComplete(string questID)
        {
            return SetQuestStatus(questID, QuestStatus.COMPLETE);
        }
    }
}