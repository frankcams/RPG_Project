using UnityEngine;

[System.Serializable]
public class Item
{
    public string itemName;
    public string description;
    public int healAmount;

    public void Use(CharacterStats target)
    {
        target.Heal(healAmount);
        Debug.Log($"{target.characterName} used {itemName} and healed {healAmount} HP.");
    }
}