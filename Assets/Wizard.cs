using UnityEngine;

public class Wizard : CharacterStats
{
    void Awake()
    {
        characterName = "Wizard";

        // Base stats for a magic user
        maxHP = 80;
        strength = 5;
        defense = 3;
        maxMP = 150;

        currentHP = GetTotalHP();
        currentMP = maxMP;

        // Starting equipment
        Equipment staff = new Equipment
        {
            equipmentName = "Arcane Staff",
            description = "Boosts magical power.",
            bonusStrength = 3,
            bonusHP = 10
        };

        Equipment robe = new Equipment
        {
            equipmentName = "Mystic Robe",
            description = "Light armor that enhances mana.",
            bonusHP = 5,
            bonusDefense = 2
        };

        EquipWeapon(staff);
        EquipArmor(robe);

        // Starting skills
        Skill fireball = new Skill
        {
            skillName = "Fireball",
            description = "Deals heavy fire damage. Costs 30 MP.",
            requiredLevel = 1
        };

        Skill manaShield = new Skill
        {
            skillName = "Mana Shield",
            description = "Absorbs damage using MP instead of HP.",
            requiredLevel = 2
        };

        skillTree.Add(fireball);
        skillTree.Add(manaShield);
    }
}