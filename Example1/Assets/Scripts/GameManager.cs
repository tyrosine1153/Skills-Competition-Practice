using Entity;

public class GameManager : Singleton<GameManager>
{
    public PlayerCharacter playerCharacter;
    public Stage currentStage;
    public int currentStageNum;
    public int score;

    protected override void OnAwake()
    {
        currentStage = FindObjectOfType<Stage>();
        playerCharacter = FindObjectOfType<PlayerCharacter>();
    }

    private void Reset()
    {
        currentStageNum = 1;
        score = 0;
    }

    public void GameOver()
    {
    }
}