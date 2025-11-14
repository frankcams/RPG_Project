using UnityEngine;
using System.Collections.Generic;

public class EnemyAI : MonoBehaviour
{
    public CharacterStats enemyStats;

    public void TakeTurn(List<CharacterStats> playerParty)
    {
        if (enemyStats.IsDead())
        {
            Debug.Log($"{enemyStats.characterName} is dead. Skipping turn.");
            return;
        }

        // Choose a valid target
        CharacterStats target = ChooseTarget(playerParty);
        if (target == null)
        {
            Debug.Log("No valid player targets.");
            return;
        }

        // Try to use a skill
        Skill chosenSkill = ChooseSkill();
        if (chosenSkill != null)
        {
            enemyStats.UseSkill(chosenSkill);
            target.TakeDamage(enemyStats.GetTotalStrength()); // Replace with skill effect if needed
            Debug.Log($"{enemyStats.characterName} used {chosenSkill.skillName} on {target.characterName}.");
        }
        else
        {
            // Basic attack
            int damage = enemyStats.GetTotalStrength();
            target.TakeDamage(damage);
            Debug.Log($"{enemyStats.characterName} attacks {target.characterName} for {damage} damage.");
        }
    }

    private CharacterStats ChooseTarget(List<CharacterStats> party)
    {
        foreach (CharacterStats member in party)
        {
            if (!member.IsDead())
                return member;
        }
        return null;
    }

    private Skill ChooseSkill()
    {
        foreach (Skill skill in enemyStats.skillTree)
        {
            if (skill.isUnlocked && !skill.IsOnCooldown() && enemyStats.HasEnoughMana(skill.manaCost))
                return skill;
        }
        return null;
    }
}