using Entity.Cell;
using Entity.Monster;
using UnityEngine;

public class PlayerBullet : Bullet
{
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        if (other.gameObject.CompareTag("Monster"))
        {
            var monster = other.gameObject.GetComponent<Monster>();
            monster.TakeDamage(damage);
            PoolManager.Instance.DestroyGameObject(gameObject, prefabType);
        }
        else if (other.gameObject.CompareTag("Cell"))
        {
            var cell = other.gameObject.GetComponent<Cell>();
            cell.TakeDamage(damage);
            PoolManager.Instance.DestroyGameObject(gameObject, prefabType);
        }
    }
}