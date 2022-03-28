public class GameManager : Singleton<GameManager>
{
    public int currentStageNum;
    public Stage currentStage;
    public int score;

    private void Start()
    {
        currentStage = FindObjectOfType<Stage>();
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