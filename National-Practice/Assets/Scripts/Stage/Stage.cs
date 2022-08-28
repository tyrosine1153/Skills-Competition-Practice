using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UI;
using UnityEngine;

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
        while (true)
        {
            yield return null;
            Timer += Time.deltaTime;
        }
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
        inputCounts[key] = Mathf.Max(--inputCounts[key], 0);
        InGameCanvas.Instance.SetInputCount(LimitedInputs.IndexOf(key), inputCounts[key]);
        if (LimitedInputs.All(keyCode => inputCounts[keyCode] <= 0))
        {
            GameManager.Instance.GameOver();
        }
    }

    #endregion

    #region Stage Flow
    
    private Coroutine _coTimer;
    private const string MapPrefabPathFormat = "Prefabs/Map/{0}";

    public void SetStage(StageModel stageData)
    {
        hpMax = stageData.hp;
        Hp = hpMax;

        InGameCanvas.Instance.StageId = stageData.stageId + 1;
        InitInputCounts(stageData.inputCount);
        Instantiate(Resources.Load<GameObject>(string.Format(MapPrefabPathFormat, stageData.stageId)), transform);

        var player = FindObjectOfType<Player>();
        var stageStartPoint = FindObjectOfType<StageStartPoint>();
        player.transform.position = stageStartPoint.transform.position;
        // Todo : 플레이어 관련 값 초기화, 이외에도 맵 이외에 Stage Scene 에서 휘발되지 않는 객체 값 초기화
    }

    public void StartStage()
    {
        _coTimer = StartCoroutine(CoTimer());
    }

    public void EndStage()
    {
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
