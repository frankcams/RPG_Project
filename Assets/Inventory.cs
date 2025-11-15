using UnityEngine;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour
{
    [Header("Currency")]
    [SerializeField] private int gold = 0;   // Player's gold amount

    [Header("Equipment")]
    [SerializeField] private List<Equipment> equipmentInventory = new List<Equipment>();

    // ✅ Public property to safely access gold
    public int Gold => gold;

    // ✅ Public property to access equipment list (read-only)
    public IReadOnlyList<Equipment> EquipmentInventory => equipmentInventory;

    /// <summary>
    /// Adds gold to the player's inventory.
    /// </summary>
    public void AddGold(int amount)
    {
        if (amount <= 0) return;

        gold += amount;
        Debug.Log($"Received {amount} gold. Total: {gold}");
    }

    /// <summary>
    /// Attempts to spend gold. Returns true if successful.
    /// </summary>
    public bool SpendGold(int amount)
    {
        if (amount <= 0) return false;

        if (gold >= amount)
        {
            gold -= amount;
            Debug.Log($"Spent {amount} gold. Remaining: {gold}");
            return true;
        }

        Debug.Log("Not enough gold!");
        return false;
    }

    /// <summary>
    /// Adds equipment to the inventory.
    /// </summary>
    public void AddEquipment(Equipment equipment)
    {
        if (equipment == null) return;

        equipmentInventory.Add(equipment);
        Debug.Log($"Added equipment: {equipment.equipmentName}");
    }
}