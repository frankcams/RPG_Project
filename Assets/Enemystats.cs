using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [Header("Basic Info")]
    public string enemyName = "Goblin";
    public int level = 1;

    [Header("Combat Stats")]
    public int maxHP = 80;
    public int currentHP;
    public int strength = 8;
    public int defense = 3;

    [Header("Rewards")]
    public int experienceReward = 50;

    void Start()
    {
        currentHP = maxHP;
    }

    public void TakeDamage(int damage)
    {
        int actualDamage = Mathf.Max(damage - defense, 1);
        currentHP -= actualDamage;
        currentHP = Mathf.Max(currentHP, 0);
        Debug.Log($"{enemyName} took {actualDamage} damage. HP left: {currentHP}");
    }

    public bool IsDead()
    {
        return currentHP <= 0;
    }

    public int GetAttackDamage()
    {
        return strength;
    }

    public int GetXPReward()
    {
        return experienceReward;
    }
}