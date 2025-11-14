using UnityEngine;

public class Assassin : CharacterStats
{
    void Awake()
    {
        characterName = "Assassin";

        // Base stats for a burst-damage rogue
        maxHP = 70;
        strength = 18;
        defense = 4;
        maxMP = 60;

        currentHP = GetTotalHP();
        currentMP = maxMP;

        // Starting equipment
        Equipment daggers = new Equipment
        {
            equipmentName = "Twin Daggers",
            description = "Light blades that boost critical damage.",
            bonusStrength = 6
        };

        Equipment cloak = new Equipment
        {
            equipmentName = "Shadow Cloak",
            description = "Enhances evasion and stealth.",
            bonusDefense = 2,
            bonusHP = 5
        };

        EquipWeapon(daggers);
        EquipArmor(cloak);

        // Starting skills
        Skill shadowStrike = new Skill
        {
            skillName = "Shadow Strike",
            description = "Deals double damage if used first. Costs 25 MP.",
            requiredLevel = 1,
            manaCost = 25,
            cooldown = 4f
        };

        Skill evasion = new Skill
        {
            skillName = "Evasion",
            description = "Avoids next attack. Costs 20 MP.",
            requiredLevel = 2,
            manaCost = 20,
            cooldown = 6f
        };

        skillTree.Add(shadowStrike);
        skillTree.Add(evasion);
    }
}