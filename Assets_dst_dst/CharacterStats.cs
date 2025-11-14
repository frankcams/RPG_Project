using System.Collections.ObjectModel;
using UnityEngine;
public class CharacterStats : MonoBehaviour
{
    public int maxHP = 100;
    public int currentHP;

    void Start()
    {
        currentHP = maxHP;
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        currentHP = Mathf.Max(currentHP, 0); // Prevent negative HP
        Debug.Log($"{gameObject.name} took {damage} damage. HP left: {currentHP}");
    }

    public bool IsDead()
    {
        return currentHP <= 0;
    }
}