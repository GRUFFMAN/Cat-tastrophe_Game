using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Quests;

public class CompleteQuest : MonoBehaviour
{
    public GameObject youWin;
    public void CompleteQuestRedQuest()
    {
        QuestManager.instance.SetQuestComplete("redcube");
    }

    public void CompleteQuestBlueQuest()
    {
        QuestManager.instance.SetQuestComplete("bluecube");
    }

    public void CompleteSubQuest(string subquest)
    {
        QuestManager.instance.SetQuestComplete(subquest);
    }

    public void OnAllQuestsComplete()
    {
        youWin.SetActive(true);
    }
}
