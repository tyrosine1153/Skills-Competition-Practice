using System;
using System.Collections.Generic;
using UnityEngine;

public enum PrefabType
{
    Bacteria,
    Germ,
    Virus,
    Cancer,

    RedCell,
    WhiteCell,
    Coronavirus,
    CoronavirusVariety,

    PlayerBullet,
    MonsterBullet,

    Item
}

public class PoolManager : Singleton<PoolManager>
{
    #region SetPoolData

    [Tooltip("0. Bacteria\n" +
             "1. Germ\n" +
             "2. Virus\n" +
             "3. Cancer\n" +
             "4. RedCell\n" +
             "5. WhiteCell\n" +
             "6. Coronavirus\n" +
             "7. CoronavirusVariety\n" +
             "8. PlayerBullet\n" +
             "9. MonsterBullet\n" +
             "10. Item")]
    public GameObject[] prefabList;

    private readonly List<Queue<GameObject>> _poolList =
        new List<Queue<GameObject>>(Enum.GetValues(typeof(PrefabType)).Length);

    protected override void Awake()
    {
        base.Awake();

        for (var i = 0; i < _poolList.Capacity; i++)
        {
            _poolList.Add(new Queue<GameObject>());
        }
    }

    #endregion

    public void DestroyGameObject(GameObject go, PrefabType prefabType)
    {
        var pool = _poolList[(int)prefabType];
        pool.Enqueue(go);
        go.SetActive(false);
    }

    public GameObject CreateGameObject(PrefabType prefabType)
    {
        var pool = _poolList[(int)prefabType];

        GameObject prefab;
        if (pool.Count != 0)
        {
            prefab = pool.Dequeue();
        }
        else
        {
            prefab = Instantiate(prefabList[(int)prefabType], transform);
        }

        prefab.SetActive(true);
        return prefab;
    }
}