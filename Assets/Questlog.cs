using UnityEngine;
using System.Collections.Generic;

public class QuestLog : MonoBehaviour
{
    public List<Quest> activeQuests = new List<Quest>();

    public void AddQuest(Quest quest)
    {
        if (!activeQuests.Contains(quest))
        {
            activeQuests.Add(quest);
            Debug.Log($"Quest added: {quest.questName}");
        }
    }

    public void CompleteQuest(Quest quest)
    {
        if (activeQuests.Contains(quest) && !quest.isCompleted)
        {
            quest.Complete();
            activeQuests.Remove(quest);
        }
    }
        public void CompleteQuest(Quest quest, CharacterStats player)
{
    if (activeQuests.Contains(quest) && !quest.isCompleted)
    {
        quest.Complete(player);
        activeQuests.Remove(quest);
    }
}
    }