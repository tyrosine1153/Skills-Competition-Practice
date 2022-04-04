using Entity;
using UnityEngine;

public class MonsterBullet : Bullet
{
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        if (other.gameObject.CompareTag("Player"))
        {
            var player = other.gameObject.GetComponent<PlayerCharacter>();
            player.TakeDamage(damage);
            PoolManager.Instance.DestroyGameObject(gameObject, prefabType);
        }
    }
}