using System;
using Entity;
using UnityEngine;

public class Item : MonoBehaviour
{
    public PrefabType prefabType;
    public ItemType itemType;
    public float moveSpeed = 0.5f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var player = other.gameObject.GetComponent<PlayerCharacter>();
            player.GetItem(itemType);
            PoolManager.Instance.DestroyGameObject(gameObject, prefabType);
        }
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(Vector3.down * Time.deltaTime * moveSpeed);
    }
}