using System;
using System.Collections;
using System.Collections.Generic;
using Entity.Monster;
using UI;
using UnityEngine;
using Random = UnityEngine.Random;

public class Stage : Singleton<Stage>
{
    public int stageNum;

    protected override void Awake()
    {
        base.Awake();
        CurrentPain = startPain;

        if (GameManager.Instance != null)
            GameManager.Instance.GameStart();

        var length = Enum.GetValues(typeof(ItemType)).Length;
        itemSprites = new Sprite[length];
        for(int i = 0; i < length; i++)
        {
            itemSprites[i] = Resources.Load<Sprite>($"Sprites/{i}");
        }
    }

    public void OnGameStart()
    {
        StartSpawn();
    }

    public void EndStage()
    {
        StageCanvas.Instance.endPanel.SetTrigger(Close);
        StopSpawn();
        DeleteAllMonsters();
    }

    #region Pains Managing

    public float startPain;
    private float currentPain;

    public float CurrentPain
    {
        get => currentPain;
        set
        {
            StageCanvas.Instance.painGauge.value = currentPain / MAXPain;
            currentPain = Mathf.Max(value, MINPain);
            if (currentPain >= MAXPain)
            {
                currentPain = MAXPain;
                GameManager.Instance.GameOver();
            }
        }
    }

    public const int MAXPain = 100;
    private const int MINPain = 0;

    public void Heal(float amount)
    {
        CurrentPain -= amount;
        // UI
    }

    public void Hurt(float amount)
    {
        CurrentPain += amount;
        // UI
    }

    #endregion

    #region Objects Spawning

    private readonly struct SpawnData
    {
        public readonly (float min, float max) X;
        public readonly float Y;
        public readonly float SpawnStartTime;
        public readonly (float min, float max) SpawnDelayTime;

        public SpawnData(float xMin, float xMax, float y, float spawnStartTime, float spawnDelayMin,
            float spawnDelayMax)
        {
            X = (xMin, xMax);
            Y = y;
            SpawnStartTime = spawnStartTime;
            SpawnDelayTime = (spawnDelayMin, spawnDelayMax);
        }
    }

    private static readonly SpawnData[] SpawnDataList =
    {
        // Bacteria,
        new SpawnData(-7.5f, -0.5f, 5.5f, 2f, 4f, 6f),
        // Germ,
        new SpawnData(-7.5f, -0.5f, 5.5f, 8f, 8f, 10f),
        // Virus,
        new SpawnData(-7.5f, -0.5f, 5.5f, 6f, 6f, 8f),
        // Cancer,
        new SpawnData(-7.5f, -0.5f, 5.5f, 4f, 10f, 12f),

        // RedCell,
        new SpawnData(-7.5f, -0.5f, 5.5f, 2f, 2f, 5f),
        // WhiteCell,
        new SpawnData(-7.5f, -0.5f, 5.5f, 2f, 2f, 3f),

        // Coronavirus,
        new SpawnData(-4, -4, 4.5f, 15f, 0, 0),
        // CoronavirusVariety,
        new SpawnData(-4, -4, 4.5f, 20f, 0, 0),

        // Item
        new SpawnData()
    };

    private readonly Queue<Coroutine> _spawnCoroutines = new Queue<Coroutine>();
    private static readonly int Close = Animator.StringToHash("Close");
    private const int CoSpawnCount = 6;

    private Sprite[] itemSprites;

    public GameObject Spawn(PrefabType prefabType, bool isItem = false)
    {
        var mob = PoolManager.Instance.CreateGameObject(prefabType);
        if (isItem)
        {
            var item = mob.GetComponent<Item>();
            var rand = Random.Range(0, Enum.GetValues(typeof(ItemType)).Length);
            item.itemType = (ItemType)rand;
            mob.GetComponent<SpriteRenderer>().sprite = itemSprites[(int)item.itemType];
            Debug.Log($"{rand} - {item.itemType} - {itemSprites[(int)item.itemType].name} - {mob.GetComponent<SpriteRenderer>().sprite.name}");
        }
        else
        {
            var spawnData = SpawnDataList[(int)prefabType];
            mob.transform.position = new Vector3(Random.Range(spawnData.X.min, spawnData.X.max), spawnData.Y);
        }

        return mob;
    }

    private IEnumerator CoSpawn(PrefabType prefabType, bool isBoss = false)
    {
        var spawnData = SpawnDataList[(int)prefabType];

        yield return new WaitForSeconds(spawnData.SpawnStartTime);

        do
        {
            var mob = PoolManager.Instance.CreateGameObject(prefabType);
            mob.transform.position = new Vector3(Random.Range(spawnData.X.min, spawnData.X.max), spawnData.Y);
            yield return new WaitForSeconds(Random.Range(spawnData.SpawnDelayTime.min, spawnData.SpawnDelayTime.max));
        } while (!isBoss);
    }

    private void StartSpawn()
    {
        for (int i = 0; i < CoSpawnCount; i++)
        {
            _spawnCoroutines.Enqueue(StartCoroutine(CoSpawn((PrefabType)i)));
        }

        _spawnCoroutines.Enqueue(StartCoroutine(CoSpawn((PrefabType)GameManager.Instance.CurrentStageNum + 5, true)));
    }

    private void StopSpawn()
    {
        while (_spawnCoroutines.Count == 0)
        {
            StopCoroutine(_spawnCoroutines.Dequeue());
        }
    }

    public void DeleteAllMonsters()
    {
        var monsters = FindObjectsOfType<Monster>();
        foreach (var monster in monsters)
        {
            monster.Die();
        }
    }

    #endregion
}