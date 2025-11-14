using UnityEngine;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour
{
    public int gold = 0;

    // ✅ Equipment inventory
    public List<Equipment> equipmentInventory = new List<Equipment>();

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

    // ✅ Add equipment to inventory
    public void AddEquipment(Equipment equipment)
    {
        if (equipment != null)
        {
            equipmentInventory.Add(equipment);
            Debug.Log($"Added equipment: {equipment.equipmentName}");
        }
    }
}