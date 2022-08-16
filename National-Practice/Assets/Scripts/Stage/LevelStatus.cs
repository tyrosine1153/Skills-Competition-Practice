public class LevelStatus
{
    public int currentLevel { get; private set; }
    public int currentExperience { get; private set; }

    private int[] _levelUpRequirements;

    public void AddExperience(int experience)
    {
        if (experience < 0) return;
        
        currentExperience += experience;
        while (currentExperience >= _levelUpRequirements[currentLevel])
        {
            currentExperience -= _levelUpRequirements[currentLevel];
            currentLevel++;

            if (currentLevel >= _levelUpRequirements.Length)
            {
                currentLevel = _levelUpRequirements.Length - 1;
                currentExperience = 0;
                return;
            }
        }
    }
}
