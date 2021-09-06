using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quests
{


    [System.Serializable]
    public enum QuestStatus
    {
        NO_STATUS, //this is for errors.
        NOT_STARTED,
        IN_PROGRESS,
        COMPLETE
    }

    [CreateAssetMenu(fileName = "NewQuestName", menuName = "QuestDetails")]
    public class QuestDetails : ScriptableObject
    {
        public string questID;
        public string questName;
        public string questDescription;
        public QuestStatus status;
        public QuestDetails parentQuest;
    }
}