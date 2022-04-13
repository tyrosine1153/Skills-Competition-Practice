using System;
using System.Collections;
using System.Collections.Generic;
using Entity;
using Entity.Monster;
using Entity.Monster.BossMonster;
using UI;
using UnityEngine;
using Random = UnityEngine.Random;

public class Stage : Singleton<Stage>
{
    public int stageNum;

    private PlayerCharacter _playerCharacter;
    public PlayerCharacter PlayerCharacter
    {
        get
        {
            if (_playerCharacter == null)
            {
                _playerCharacter = FindObjectOfType<PlayerCharacter>();
            }

            return _playerCharacter;
        }
    }
    
    protected override void Awake()
    {
        base.Awake();

        CurrentPain = startPain;
        
        if(GameManager.Instance)
            GameManager.Instance.GameStart();
        
    }

    #region PainManaging

    public float startPain;
    private float _currentPain;
    public const float MaxPain = 100f;
    private const float MinPain = 0f;

    public float CurrentPain
    {
        get => _currentPain;
        set
        {
            _currentPain = Mathf.Clamp(value, MinPain, MaxPain);
            StageCanvas.Instance.painGauge.value = _currentPain / MaxPain;
            if (_currentPain >= MaxPain)
            {
                AudioManager.Instance.PlaySfx(SfxClip.BodyDie);
                GameManager.Instance.GameEnd(false); // game over
            }
        }
    }

    public void Heal(float amount)
    {
        if (amount < 0) return;

        CurrentPain -= amount;
    }

    public void Hurt(float amount)
    {
        if (amount < 0) return;

        CurrentPain += amount;
    }

    #endregion

    #region Spawn Managing

    private readonly Queue<Coroutine> _coroutines = new Queue<Coroutine>();
    private const int SpawnCount = 6;

    private struct SpawnData
    {
        public readonly (float, float) X;
        public readonly float Y;
        public readonly float SpawnStartTime;
        public readonly (float, float) SpawnDelayTime;

        public SpawnData((float, float) x, float y, float spawnStartTime, (float, float) spawnDelayTime)
        {
            X = x;
            Y = y;
            SpawnStartTime = spawnStartTime;
            SpawnDelayTime = spawnDelayTime;
        }
    }

    private static readonly List<SpawnData> SpawnDataList = new List<SpawnData>
    {
        // Bacteria
        new SpawnData((-1f, -7f ), 7, 2, (3, 5)),
        // Germ
        new SpawnData((-1f, -7f ), 7, 4, (5, 7)),
        // Virus
        new SpawnData((-1f, -7f ), 7, 7, (7, 9)),
        // Cancer
        new SpawnData((-1f, -7f ), 7, 10, (10, 15)),
        
        // RedCell
        new SpawnData((-1f, -7f ), 7, 2, (5, 7)),
        // WhiteCell
        new SpawnData((-1f, -7f ), 7, 2, (3, 5)),
        
        // CoronaVirus
        new SpawnData((-4f, -4f), 5, 15, (3, 5)),
        // CoronaVirusVariety
        new SpawnData((-4f, -4f), 5, 30, (3, 5)),
        
        // PlayerBullet
        new SpawnData(),
        // MonsterBullet
        new SpawnData(),

        // Item
        new SpawnData(),
    };

    public GameObject Spawn(PrefabType prefabType, bool isItem = false)
    {
        var go = PoolManager.Instance.CreateGameObject(prefabType);

        if (isItem)
        {
            var item = go.GetComponent<Item>();
            item.itemType = (ItemType) Random.Range(0, Enum.GetValues(typeof(ItemType)).Length);
            item.GetComponent<SpriteRenderer>().sprite = GameManager.Instance.itemInfos[(int) item.itemType].sprite;
        }
        else
        {
            var data = SpawnDataList[(int) prefabType];
            go.transform.position = new Vector3(Random.Range(data.X.Item1, data.X.Item2), data.Y);
        }

        return go;
    }

    private IEnumerator CoSpawn(PrefabType prefabType, bool isBoss = false)
    {
        var data = SpawnDataList[(int) prefabType];

        yield return new WaitForSeconds(data.SpawnStartTime);
        do
        {
            Spawn(prefabType);
            yield return new WaitForSeconds(Random.Range(data.SpawnDelayTime.Item1, data.SpawnDelayTime.Item2));
        } while (!isBoss);
    }

    public void StartSpawn()
    {
        for (int i = 0; i < SpawnCount; i++)
        {
            _coroutines.Enqueue(StartCoroutine(CoSpawn((PrefabType) i)));
        }

        StartCoroutine(CoSpawn((PrefabType) stageNum + SpawnCount - 1, true));
    }

    public void StopSpawn()
    {
        while (_coroutines.Count > 0)
        {
            StopCoroutine(_coroutines.Dequeue());
        }
    }

    public void KillAllMonsters()
    {
        var monsters = FindObjectsOfType<Monster>();
        foreach (var monster in monsters)
        {
            monster.Die();
        }
    }

    public void DeleteAllMonsters()
    {
        var monsters = FindObjectsOfType<Monster>();
        foreach (var monster in monsters)
        {
            PoolManager.Instance.DestroyGameObject(monster.gameObject, monster.prefabType);
        }
    }

    #endregion

    #region Stage Flow
    
    private static readonly int Close = Animator.StringToHash("Close");
    
    public void StageStart()
    {
        StartSpawn();
    }

    public void StageEnd()
    {
        StageCanvas.Instance.endPanel.SetTrigger(Close);
        StopSpawn();
        DeleteAllMonsters();
    }

    #endregion
}
