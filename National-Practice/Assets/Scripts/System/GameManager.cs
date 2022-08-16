using System;
using System.Collections;
using System.Linq;
using UnityEngine;

[Serializable]
public class StageModel
{
    [Serializable]
    public class EnemyConfig
    {
        public PrefabType prefabType;
        public float spawnStartTime;
        public float spawnDelayTime;
    }

    public int mapId;
    public int stageId;
    public string stageName;
    public float limitTime;
    public float targetHealth;
    public EnemyConfig[] enemyConfigs;
}

public class GameManager : Singleton<GameManager>
{
    public StageModel currentStageData;
    public bool isGamePlaying = false;

    public void GameOver()
    {
        GameEnd(false);
    }

    public void GameClear()
    {
        GameEnd(true);
    }

    [ContextMenu("GameStart")]
    public void TestGameStart()
    {
        GameStart(1, 1);
    }

    public void GameStart(int mapIndex, int stageIndex)
    {
        if (isGamePlaying) return;
        isGamePlaying = true;

        var stageList = new StageModel[5]; // Todo : Define stage list
        
        var stageData = stageList.First(stage => stage.mapId == mapIndex && stage.stageId == stageIndex);
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

        currentStageData = null;

        yield break;
    }
}
