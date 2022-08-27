using System;
using System.Collections;
using System.Linq;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class StageModel
{
    // [Serializable]
    // public class EnemyConfig
    // {
    //     public PrefabType prefabType;
    //     public float spawnStartTime;
    //     public float spawnDelayTime;
    // }
    // public EnemyConfig[] enemyConfigs;

    public int stageId;
    public int hp;
    public int inputCount;
    
    public StageModel(int stageId, int hp, int inputCount)
    {
        this.stageId = stageId;
        this.hp = hp;
        this.inputCount = inputCount;
    }
}

public class GameManager : Singleton<GameManager>
{
    public StageModel currentStageData;
    public bool isGamePlaying = false;
    public int savedStageId;
    private const string SavedStageIdKey = "SAVED_STAGE_ID";

    private static readonly StageModel[] StageModels =
    {
        new (0, 5, 5),
        new (1, 5, 5),
        new (2, 5, 5),
        new (3, 5, 5),
        new (4, 5, 5),
    };
    // Todo : Define stage list

    #region Game Flow

    public void GameOver()
    {
        GameEnd(false);
    }

    public void GameClear()
    {
        GameEnd(true);
    }

    public void GameStart()
    {
        if (isGamePlaying) return;
        isGamePlaying = true;
        
        var stageData = StageModels.First(stage => stage.stageId == savedStageId);
        currentStageData = stageData;

        // Todo : 연출

        StartCoroutine(CoGameStart());
    }

    private IEnumerator CoGameStart()
    {
        while (!Stage.IsInitialized)
        {
            yield return null;
        }

        Stage.Instance.SetStage(currentStageData);

        yield return new WaitForSeconds(0f);

        // Todo : 대충 지연과 연출

        Stage.Instance.StartStage();

        yield break;
    }

    public void GameEnd(bool isGameClear)
    {
        if (!isGamePlaying) return;
        isGamePlaying = false;

        // Todo : 연출

        StartCoroutine(CoGameEnd(isGameClear));
    }

    private IEnumerator CoGameEnd(bool isGameClear)
    {
        Stage.Instance.EndStage();

        // Todo : 대충 지연과 연출

        if (isGameClear)
        {
            SaveData(savedStageId + 1);
            SceneManagerEx.Instance.LoadScene(savedStageId < 5 ? SceneType.InGame : SceneType.End);
        }
        else
        {
            SaveData(0);
            SceneManagerEx.Instance.LoadScene(SceneType.End);
            // Todo : End 화면에서 게임 결과에 따라 UI가 나타날 수 있도록 값을 전달해야 함
        }
        currentStageData = null;

        yield break;
    }
    
    #endregion
    
    [ContextMenu("GameStart")]
    public void TestGameStartFromZero()
    {
        SaveData(0);
        GameStart();
    }

    #region Data Managing
    
    public void SaveData(int stageId)
    {
        savedStageId = stageId;
        PlayerPrefs.SetInt(SavedStageIdKey, savedStageId);
    }

    public int LoadData()
    {
        savedStageId = PlayerPrefs.GetInt(SavedStageIdKey, 0);
        return savedStageId;
    }

    private void UpdateScoreRank(string playerName, int score)
    {
        // Todo : 저장해 두었던 점수 랭킹 가져와서 갱신하고 저장 + Json 입출력 기능 넣기
        if (EndCanvas.IsInitialized)
        {
            EndCanvas.Instance.UpdateScoreRankBoard();
        }
    }

    #endregion
}
