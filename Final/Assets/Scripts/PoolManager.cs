using System;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : Singleton<PoolManager>
{
    public GameObject[] prefabList;
    private readonly List<Queue<GameObject>> _poolList = new List<Queue<GameObject>>();

    protected override void Awake()
    {
        base.Awake();

        for (int i = 0; i < Enum.GetValues(typeof(PrefabType)).Length; i++)
        {
            _poolList.Add(new Queue<GameObject>());
        }
    }

    public GameObject CreateGameObject(PrefabType prefabType)
    {
        var pool = _poolList[(int) prefabType];

        GameObject go;
        if (pool.Count == 0)
        {
            go = Instantiate(prefabList[(int) prefabType], transform);
        }
        else
        {
            go = pool.Dequeue();
        }
        go.SetActive(true);
        return go;
    }

    public void DestroyGameObject(GameObject go, PrefabType prefabType)
    {
        var pool = _poolList[(int) prefabType];
        pool.Enqueue(go);
        go.SetActive(false);
    }
}
