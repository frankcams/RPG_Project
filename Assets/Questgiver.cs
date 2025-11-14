using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    public Quest quest;
    private bool questGiven = false;

    public void GiveQuest(CharacterStats player)
    {
        if (!questGiven)
        {
            QuestLog log = player.GetComponent<QuestLog>();
            if (log != null)
            {
                log.AddQuest(quest);
                questGiven = true;
                Debug.Log($"{quest.questName} has been added to your quest log.");

                // ✅ Complete the quest and give rewards
                quest.Complete(player);
            }
        }
        else
        {
            Debug.Log("You've already accepted this quest.");
        }
    }
}