using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

// 인게임에서만 하는 모든 시스템 (인게임 전용 GameManager)
public class Stage : Singleton<Stage>
{
    private void Update()
    {
        if (Timer < timerMax)
        {
            Timer += Time.deltaTime;
        }
    }

    #region Stage Flow Data

    public const float TargetHpMin = 0;
    public float targetHpMax;
    private float _targetHp;

    public float TreeHp
    {
        get => _targetHp;
        set
        {
            _targetHp = Mathf.Clamp(value, TargetHpMin, targetHpMax);
            // Todo : UI에 표시
            if (_targetHp <= TargetHpMin)
            {
                GameManager.Instance.GameOver();
            }
        }
    }

    public const float TimerMin = 0;
    public float timerMax;
    private float _timer;

    public float Timer
    {
        get => _timer;
        set
        {
            _timer = Mathf.Clamp(value, TimerMin, timerMax);
            // Todo : UI에 표시
            if (_timer >= timerMax)
            {
                GameManager.Instance.GameClear();
            }
        }
    }

    #endregion

    #region Spawn Managing

    [SerializeField] private Transform[] spawnPosition;

    private Vector2 RandomSidePosition => spawnPosition[Random.Range(1, spawnPosition.Length)].position;

    private readonly Queue<Coroutine> _coroutines = new Queue<Coroutine>();

    public GameObject Spawn(PrefabType prefabType)
    {
        var go = PoolManager.Instance.CreateGameObject(prefabType);
        go.transform.position = RandomSidePosition;

        return go;
    }

    private IEnumerator CoSpawn(PrefabType prefabType)
    {
        var data = GameManager.Instance.currentStageData.enemyConfigs.First(e => e.prefabType == prefabType);

        yield return new WaitForSeconds(data.spawnStartTime);
        while (true)
        {
            Spawn(prefabType);
            yield return new WaitForSeconds(data.spawnDelayTime);
        }
    }

    private void StartSpawn()
    {
        var prefabs = GameManager.Instance.currentStageData.enemyConfigs.Select(e => e.prefabType);
        foreach (var prefab in prefabs)
        {
            _coroutines.Enqueue(StartCoroutine(CoSpawn(prefab)));
        }
    }

    private void StopSpawn()
    {
        while (_coroutines.Count > 0)
        {
            StopCoroutine(_coroutines.Dequeue());
        }
    }

    #endregion

    #region Stage Flow

    public void SetStage(StageModel stageData)
    {
        timerMax = stageData.limitTime;
        targetHpMax = stageData.targetHealth;
        Timer = TimerMin;
        TreeHp = targetHpMax;
    }

    public void StartStage()
    {
        StartSpawn();
    }

    public void EndStage()
    {
        StopSpawn();
    }

    #endregion
}
