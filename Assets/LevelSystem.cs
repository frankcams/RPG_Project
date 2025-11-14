using UnityEngine;

public class LevelSystem
{
    public int CurrentLevel { get; private set; } = 1;
    public int CurrentXP { get; private set; } = 0;
    public int XPToNextLevel { get; private set; } = 100;

    public delegate void LevelUpHandler(int newLevel);
    public event LevelUpHandler onLevelUp;

    public void GainExperience(int xp)
    {
        CurrentXP += xp;

        while (CurrentXP >= XPToNextLevel)
        {
            CurrentXP -= XPToNextLevel;
            CurrentLevel++;
            XPToNextLevel += 50;

            onLevelUp?.Invoke(CurrentLevel);
        }
    }
}