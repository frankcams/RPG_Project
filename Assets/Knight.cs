using UnityEngine;

public class Knight : CharacterStats
{
    void Awake()
    {
        characterName = "Knight";
        maxHP = 150;
        currentHP = GetTotalHP();
        strength = 15;
        defense = 10;

        // Starting equipment
        Equipment sword = new Equipment
        {
            equipmentName = "Steel Sword",
            description = "A reliable blade for close combat.",
            bonusStrength = 5
        };

        Equipment armor = new Equipment
        {
            equipmentName = "Knight's Armor",
            description = "Heavy armor that boosts defense.",
            bonusHP = 20,
            bonusDefense = 5
        };

        EquipWeapon(sword);
        EquipArmor(armor);

        // Starting skill tree
        Skill shieldBash = new Skill
        {
            skillName = "Shield Bash",
            description = "Stun the enemy for one turn.",
            requiredLevel = 1
        };

        Skill ironWill = new Skill
        {
            skillName = "Iron Will",
            description = "Reduce all incoming damage by 25% for 2 turns.",
            requiredLevel = 3
        };

        skillTree.Add(shieldBash);
        skillTree.Add(ironWill);
    }
}