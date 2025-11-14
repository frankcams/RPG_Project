using UnityEngine;
using System.Collections.Generic;

public class CombatManager : MonoBehaviour
{
    public PartyManager partyManager;
    public List<CharacterStats> enemies = new List<CharacterStats>();

    private int currentTurnIndex = 0;
    private bool isPlayerTurn = true;

    void Start()
    {
        StartCombat();
    }

    public void StartCombat()
    {
        currentTurnIndex = 0;
        isPlayerTurn = true;
        Debug.Log("Combat started. Player's turn begins.");
        BeginPlayerTurn();
    }

    public void NextTurn()
    {
        currentTurnIndex++;

        if (isPlayerTurn)
        {
            if (currentTurnIndex >= partyManager.partyMembers.Count)
            {
                currentTurnIndex = 0;
                isPlayerTurn = false;
                Debug.Log("Enemy's turn begins.");
                BeginEnemyTurn();
            }
            else
            {
                BeginPlayerTurn();
            }
        }
        else
        {
            if (currentTurnIndex >= enemies.Count)
            {
                currentTurnIndex = 0;
                isPlayerTurn = true;
                Debug.Log("Player's turn begins.");
                BeginPlayerTurn();
            }
            else
            {
                BeginEnemyTurn();
            }
        }
    }

    void BeginPlayerTurn()
    {
        CharacterStats player = partyManager.partyMembers[currentTurnIndex];
        if (player.IsDead())
        {
            Debug.Log($"{player.characterName} is dead. Skipping turn.");
            NextTurn();
            return;
        }

        partyManager.SwitchToMember(currentTurnIndex);
        Debug.Log($"It's {player.characterName}'s turn.");
        // Wait for player input (e.g., skill, attack, item)
    }

    void BeginEnemyTurn()
    {
        CharacterStats enemy = enemies[currentTurnIndex];
        if (enemy.IsDead())
        {
            Debug.Log($"{enemy.characterName} is dead. Skipping turn.");
            NextTurn();
            return;
        }

        CharacterStats target = partyManager.partyMembers.Find(p => !p.IsDead());
        if (target == null)
        {
            Debug.Log("All players are dead. Game over.");
            return;
        }

        Skill skill = enemy.skillTree.Find(s => s.isUnlocked && !s.IsOnCooldown() && enemy.HasEnoughMana(s.manaCost));
        if (skill != null)
        {
            enemy.UseSkill(skill);
            target.TakeDamage(enemy.GetTotalStrength()); // Replace with skill effect if needed
            Debug.Log($"{enemy.characterName} used {skill.skillName} on {target.characterName}.");
        }
        else
        {
            int damage = enemy.GetTotalStrength();
            target.TakeDamage(damage);
            Debug.Log($"{enemy.characterName} attacks {target.characterName} for {damage} damage.");
        }

        NextTurn();
    }

    public bool IsPlayerTurn()
    {
        return isPlayerTurn;
    }
}