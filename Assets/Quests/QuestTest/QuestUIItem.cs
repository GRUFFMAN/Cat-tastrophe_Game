using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Quests;
using UnityEngine.UI;
using TMPro;
public class QuestUIItem : TrackedQuestUIItem
{
    public TextMeshProUGUI titleText;
    public GameObject checkMark;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Init(QuestTrackerUI trackerUI, QuestDetails questdetails, int questSubLevel = 0)
    {
        base.Init(trackerUI, questdetails);
        titleText.text = questdetails.questName;
        for (int i = 0; i < questSubLevel; i++)
        {
            titleText.text = "-" + titleText.text;
        }
        Refresh(questdetails.status);
    }

    public override void Refresh(QuestStatus status)
    {
        base.Refresh(status);
        if (status == QuestStatus.COMPLETE)
        {
            checkMark.SetActive(true);
            titleText.fontStyle = FontStyles.Strikethrough;
        }
        else
        {
            checkMark.SetActive(false);
            titleText.fontStyle = FontStyles.Normal;
        }
    }
}
