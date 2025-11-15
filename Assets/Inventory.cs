using UnityEngine;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour
{
    public int gold = 0;

    // ✅ Consumable items (potions, etc.)
    public List<Item> itemInventory = new List<Item>();

    // ✅ Equipment (weapons, armor, etc.)
    public List<Equipment> equipmentInventory = new List<Equipment>();

    // 💰 Gold management
    public void AddGold(int amount)
    {
        gold += amount;
        Debug.Log($"Received {amount} gold. Total: {gold}");
    }

    public bool SpendGold(int amount)
    {
        if (gold >= amount)
        {
            gold -= amount;
            return true;
        }
        return false;
    }

    // 🎒 Item management
    public void AddItem(Item item)
    {
        if (item != null)
        {
            itemInventory.Add(item);
            Debug.Log($"Added item: {item.itemName}");
        }
    }

    public void UseItem(int index, CharacterStats target)
    {
        if (index < 0 || index >= itemInventory.Count) return;

        Item item = itemInventory[index];
        item.Use(target);
        itemInventory.RemoveAt(index);
    }

    // 🛡️ Equipment management
    public void AddEquipment(Equipment equipment)
    {
        if (equipment != null)
        {
            equipmentInventory.Add(equipment);
            Debug.Log($"Added equipment: {equipment.equipmentName}");
        }
    }
}