using UnityEngine;
using System.Collections.Generic;

public class CharacterStats : MonoBehaviour
{
    [Header("Basic Info")]
    public string characterName = "Hero";

    [Header("Base Stats")]
    public int maxHP = 100;
    public int currentHP;
    public int maxMP = 50;
    public int currentMP;
    public int strength = 10;
    public int defense = 5;

    [Header("Leveling")]
    public LevelSystem levelSystem = new LevelSystem();
    public int skillPoints = 0;
    public int CurrentLevel => levelSystem.CurrentLevel;

    [Header("Combat State")]
    public bool isDefending = false;

    [Header("Inventory & Skills")]
    public List<Item> inventory = new List<Item>();
    public List<Skill> skillTree = new List<Skill>();

    [Header("Equipment")]
    public Equipment equippedWeapon;
    public Equipment equippedArmor;

    // -------------------- Unity Lifecycle --------------------
    void Start()
    {
        currentHP = GetTotalHP();
        currentMP = maxMP;
        levelSystem.onLevelUp += OnLevelUp;
    }

    // -------------------- Derived Stats --------------------
    public int GetTotalHP()
    {
        int bonus = 0;
        if (equippedWeapon != null) bonus += equippedWeapon.bonusHP;
        if (equippedArmor != null) bonus += equippedArmor.bonusHP;
        return maxHP + bonus;
    }

    public int GetTotalStrength()
    {
        int bonus = 0;
        if (equippedWeapon != null) bonus += equippedWeapon.bonusStrength;
        if (equippedArmor != null) bonus += equippedArmor.bonusStrength;
        return strength + bonus;
    }

    public int GetTotalDefense()
    {
        int bonus = 0;
        if (equippedWeapon != null) bonus += equippedWeapon.bonusDefense;
        if (equippedArmor != null) bonus += equippedArmor.bonusDefense;
        return defense + bonus;
    }

    // -------------------- Combat --------------------
    public void TakeDamage(int incomingDamage)
    {
        int actualDamage = Mathf.Max(incomingDamage - GetTotalDefense(), 1);
        if (isDefending) actualDamage /= 2;

        currentHP = Mathf.Max(currentHP - actualDamage, 0);
        Debug.Log($"{characterName} took {actualDamage} damage. HP left: {currentHP}");
    }

    public void Heal(int amount)
    {
        currentHP = Mathf.Min(currentHP + amount, GetTotalHP());
        Debug.Log($"{characterName} healed {amount} HP. Current HP: {currentHP}");
    }

    public bool IsDead() => currentHP <= 0;

    public void Defend()
    {
        isDefending = true;
        Debug.Log($"{characterName} is defending this turn!");
    }

    public void ResetDefend() => isDefending = false;

    // -------------------- Mana --------------------
    public bool HasEnoughMana(int cost) => currentMP >= cost;

    public void UseMana(int cost)
    {
        currentMP = Mathf.Max(currentMP - cost, 0);
        Debug.Log($"{characterName} used {cost} MP. Remaining MP: {currentMP}");
    }

    public void RestoreMana(int amount)
    {
        currentMP = Mathf.Min(currentMP + amount, maxMP);
        Debug.Log($"{characterName} restored {amount} MP. Current MP: {currentMP}");
    }

    // -------------------- Leveling --------------------
    public void GainXP(int xp) => levelSystem.GainExperience(xp);

    private void OnLevelUp(int newLevel)
    {
        maxHP += 20;
        maxMP += 10;
        strength += 2;
        defense += 1;

        currentHP = GetTotalHP();
        currentMP = maxMP;
        skillPoints++;

        Debug.Log($"{characterName} leveled up to {newLevel}!");
        Debug.Log($"New Stats — HP: {GetTotalHP()}, MP: {maxMP}, STR: {GetTotalStrength()}, DEF: {GetTotalDefense()}");
        Debug.Log($"Skill Points available: {skillPoints}");
    }

    // -------------------- Inventory --------------------
    public void UseItem(int index)
    {
        if (index < 0 || index >= inventory.Count) return;

        Item item = inventory[index];
        item.Use(this);
        inventory.RemoveAt(index);
    }

    // -------------------- Equipment --------------------
    public void EquipWeapon(Equipment weapon)
    {
        equippedWeapon = weapon;
        Debug.Log($"{characterName} equipped weapon: {weapon.equipmentName}");
    }

    public void EquipArmor(Equipment armor)
    {
        equippedArmor = armor;
        Debug.Log($"{characterName} equipped armor: {armor.equipmentName}");
    }

    // -------------------- Skills --------------------
    public void TryUnlockSkill(Skill skill)
    {
        if (skillPoints <= 0)
        {
            Debug.Log("Not enough skill points.");
            return;
        }

        if (skill.CanUnlock(CurrentLevel))
        {
            skill.Unlock();
            skillPoints--;
        }
        else
        {
            Debug.Log("Requirements not met for unlocking skill.");
        }
    }

    public void UseSkill(Skill skill)
    {
        if (!skill.isUnlocked)
        {
            Debug.Log($"Skill {skill.skillName} is not unlocked.");
            return;
        }

        if (skill.IsOnCooldown())
        {
            float remaining = (skill.lastUsedTime + skill.cooldown) - Time.time;
            Debug.Log($"{skill.skillName} is on cooldown. {remaining:F1}s remaining.");
            return;
        }

        if (!HasEnoughMana(skill.manaCost))
        {
            Debug.Log($"Not enough mana to use {skill.skillName}.");
            return;
        }

        UseMana(skill.manaCost);
        skill.TriggerCooldown();

        Debug.Log($"{characterName} used skill: {skill.skillName}!");
        // TODO: Add actual skill effect logic here
    }
}