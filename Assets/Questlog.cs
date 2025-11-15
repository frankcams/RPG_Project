using UnityEngine;
using System.Collections.Generic;

public class QuestLog : MonoBehaviour
{
    public List<Quest> activeQuests = new List<Quest>();

    // ✅ Add a quest to the log
    public void AddQuest(Quest quest)
    {
        if (!activeQuests.Contains(quest))
        {
            activeQuests.Add(quest);
            Debug.Log($"Quest added: {quest.questName}");
        }
    }

    // ✅ Complete a quest, requires the player reference
    public void CompleteQuest(Quest quest, CharacterStats player)
    {
        if (activeQuests.Contains(quest) && !quest.isCompleted)
        {
            quest.Complete(player);   // ✅ passes player correctly
            activeQuests.Remove(quest);
            Debug.Log($"Quest completed and removed: {quest.questName}");
        }
    }
}