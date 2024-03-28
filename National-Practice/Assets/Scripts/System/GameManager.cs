using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UI;
using UnityEngine;

[Serializable]
public class StageModel
{
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

[Serializable]
public class ScoreRanking
{
    public List<(string playerName, int score)> value;
}

public class GameManager : Singleton<GameManager>
{
    public StageModel currentStageData;
    public bool isGamePlaying = false;
    public int savedStageId;
    private const string SavedStageIdKey = "SAVED_STAGE_ID";

    private static readonly StageModel[] StageModels =
    {
        new (0, 6, 100),
        new (1, 6, 6),
        new (2, 6, 7),
        new (3, 6, 8),
        new (4, 6, 9),
    };
    // Todo : Define stage list
    
    public ScoreRanking scoreRanking = new ();
    // Todo : Score 값 관리

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
            if (savedStageId < 5)
            {
                SaveStageData(savedStageId + 1);
                SceneManagerEx.Instance.LoadScene(SceneType.InGame);
            }
            else
            {
                SceneManagerEx.Instance.LoadScene(SceneType.End);
                // Todo : End 화면에서 게임 결과에 따라 UI가 나타날 수 있도록 값을 전달해야 함
            }
        }
        else
        {
            SaveStageData(0);
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
        SaveStageData(0);
        GameStart();
    }

    #region Data Managing
    
    public void SaveStageData(int stageId)
    {
        savedStageId = stageId;
        PlayerPrefs.SetInt(SavedStageIdKey, savedStageId);
    }

    public int LoadStageData()
    {
        savedStageId = PlayerPrefs.GetInt(SavedStageIdKey, 0);
        return savedStageId;
    }

    public void UpdateScoreRank(string playerName, int score)
    {
        scoreRanking = LoadByJson<ScoreRanking>("Data", "ScoreRanking");
        scoreRanking.value.Add((playerName, score));
        scoreRanking.value = scoreRanking.value.OrderByDescending(rank => rank.score).Take(3).ToList();
        SaveByJson("Data", "ScoreRanking", scoreRanking);
        
        if (EndCanvas.IsInitialized)
        {
            EndCanvas.Instance.UpdateScoreRankBoard();
        }
    }
    
    private static void SaveByJson<T>(string filePath, string fileName, T obj)
    {
        File.WriteAllText($"{filePath}/{fileName}.json", JsonUtility.ToJson(obj, true));
    }
    
    private static T LoadByJson<T>(string filePath, string fileName)
    {
        var path = $"{Application.dataPath}/{filePath}/{fileName}.json";
        if (File.Exists(path))
        {
            return JsonUtility.FromJson<T>(File.ReadAllText(path));
        }

        return default;
    }

    #endregion
}
