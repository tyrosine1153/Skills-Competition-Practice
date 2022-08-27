using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UI;
using UnityEngine;
using Random = UnityEngine.Random;

public class Stage : Singleton<Stage>
{
    #region Stage Flow Data
   
    private float _timer;
    public float Timer
    {
        get => _timer;
        set
        {
            _timer = Mathf.Max(value, 0);
            InGameCanvas.Instance.PlayTime = (int)_timer;
        }
    }
    private IEnumerator CoTimer()
    {
        yield return null;
        Timer += Time.deltaTime;
    }
    
    private int _hp;
    public int hpMax;
    private const int HpMin = 0;
    public int Hp
    {
        get => _hp;
        set
        {
            _hp = Mathf.Clamp(value, HpMin, hpMax);
            InGameCanvas.Instance.Hp = _hp;
            if (_hp <= HpMin)
            {
                GameManager.Instance.GameOver();
            }
        }
    }

    public static readonly List<KeyCode> LimitedInputs = new()
    {
        KeyCode.LeftArrow, 
        KeyCode.RightArrow, 
        KeyCode.UpArrow, 
        KeyCode.DownArrow,
        KeyCode.Space, // 점프
        KeyCode.Z, // 매달리기
    };

    public readonly Dictionary<KeyCode, int> inputCounts = new();
    
    private void InitInputCounts(int inputCountStart)
    {
        inputCounts.Clear();
        foreach (var key in LimitedInputs)
        {
            inputCounts.Add(key, inputCountStart);
        }
    }

    public bool CanInputKey(KeyCode key) => inputCounts[key] > 0;
    
    public void CountInputKey(KeyCode key)
    {
        inputCounts[key]--;
        InGameCanvas.Instance.SetInputCount(key, inputCounts[key]);
        if (LimitedInputs.All(keyCode => inputCounts[keyCode] <= 0))
        {
            GameManager.Instance.GameOver();
        }
    }

    #endregion

    #region Spawn Managing
    //
    // [SerializeField] private Transform[] spawnPosition;
    //
    // private Vector2 RandomSidePosition => spawnPosition[Random.Range(1, spawnPosition.Length)].position;
    //
    // private readonly Queue<Coroutine> _coroutines = new Queue<Coroutine>();
    //
    // public GameObject Spawn(PrefabType prefabType)
    // {
    //     var go = PoolManager.Instance.CreateGameObject(prefabType);
    //     go.transform.position = RandomSidePosition;
    //
    //     return go;
    // }
    //
    // private IEnumerator CoSpawn(PrefabType prefabType)
    // {
    //     var data = GameManager.Instance.currentStageData.enemyConfigs.First(e => e.prefabType == prefabType);
    //
    //     yield return new WaitForSeconds(data.spawnStartTime);
    //     while (true)
    //     {
    //         Spawn(prefabType);
    //         yield return new WaitForSeconds(data.spawnDelayTime);
    //     }
    // }
    //
    // private void StartSpawn()
    // {
    //     var prefabs = GameManager.Instance.currentStageData.enemyConfigs.Select(e => e.prefabType);
    //     foreach (var prefab in prefabs)
    //     {
    //         _coroutines.Enqueue(StartCoroutine(CoSpawn(prefab)));
    //     }
    // }
    //
    // private void StopSpawn()
    // {
    //     while (_coroutines.Count > 0)
    //     {
    //         StopCoroutine(_coroutines.Dequeue());
    //     }
    // }
    //
    #endregion

    #region Stage Flow
    
    private Coroutine _coTimer;
    private const string MapPrefabPathFormat = "Prefabs/Map/{0}";

    public void SetStage(StageModel stageData)
    {
        hpMax = stageData.hp;
        Hp = hpMax;
        
        InitInputCounts(stageData.inputCount);
        Instantiate(Resources.Load<GameObject>(string.Format(MapPrefabPathFormat, stageData.stageId)), transform);

        var player = FindObjectOfType<Player>();
        var stageStartPoint = FindObjectOfType<StageStartPoint>();
        player.transform.position = stageStartPoint.transform.position;
        // Todo : 플레이어 관련 값 초기화, 이외에도 맵 이외에 Stage Scene 에서 휘발되지 않는 객체 값 초기화
    }

    public void StartStage()
    {
        // StartSpawn();
        _coTimer = StartCoroutine(CoTimer());
    }

    public void EndStage()
    {
        // StopSpawn();
        StopCoroutine(_coTimer);
    }
    #endregion

    protected override void Awake()
    {
        base.Awake();

        if (GameManager.IsInitialized)
        {
            GameManager.Instance.GameStart();
        }
    }
}
