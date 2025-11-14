using UnityEngine;

[System.Serializable]
public class Skill
{
    public string skillName;
    public string description;
    public bool isUnlocked = false;
    public int requiredLevel = 1;
    public Skill[] prerequisites;

    public int manaCost = 0;
    public float cooldown = 0f; // in seconds
    public float lastUsedTime = -999f;

    public void Unlock()
    {
        isUnlocked = true;
        Debug.Log($"Skill unlocked: {skillName}");
    }

    public bool CanUnlock(int currentLevel)
    {
        if (isUnlocked || currentLevel < requiredLevel) return false;

        foreach (Skill prereq in prerequisites)
        {
            if (prereq != null && !prereq.isUnlocked)
                return false;
        }

        return true;
    }

    public bool IsOnCooldown()
    {
        return Time.time < lastUsedTime + cooldown;
    }

    public void TriggerCooldown()
    {
        lastUsedTime = Time.time;
    }
}