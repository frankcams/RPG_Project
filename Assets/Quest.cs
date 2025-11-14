using UnityEngine;

[CreateAssetMenu(fileName = "NewQuest", menuName = "Quests/Quest")]
public class Quest : ScriptableObject
{
    public string questName;
    [TextArea(3, 5)]
    public string description;
    public bool isCompleted = false;

    [Header("Rewards")]
    public int rewardXP = 100;
    public int rewardGold = 50;
    public Equipment rewardWeapon; // Optional weapon reward

    public void Complete(CharacterStats player)
    {
        if (isCompleted) return;

        isCompleted = true;
        Debug.Log($"Quest Completed: {questName}");

        // Give rewards
        player.GainXP(rewardXP);
        player.GetComponent<PlayerInventory>()?.AddGold(rewardGold);

        if (rewardWeapon != null)
        {
            player.inventory.Add(rewardWeapon);
            Debug.Log($"Received weapon: {rewardWeapon.equipmentName}");
        }
    }
}