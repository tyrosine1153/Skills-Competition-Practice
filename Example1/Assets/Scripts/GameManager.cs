using Entity;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public PlayerCharacter PlayerCharacter { get; private set; }
    public Stage CurrentStage { get; private set; }
    public int CurrentStageNum { get; private set; }
    public int Score { get; private set; } = 0;

    public void AddScore(int score)
    {
        Score += score;
    }

    protected override void OnAwake()
    {
        CurrentStage = FindObjectOfType<Stage>();
        PlayerCharacter = FindObjectOfType<PlayerCharacter>();
        Score = 0;
        
        
    }

    private void Reset()
    {
        CurrentStageNum = 1;
        Score = 0;
    }

    public void GameOver()
    {

    }

    public void GameClear()
    {
        // 체력, 고통 게이지, 얻은 아이템 따라서 보너스 스코어 추가 정산
        CurrentStage.EndStage();
        SceneManager.LoadScene(++CurrentStageNum);
    }
}