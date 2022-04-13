using UnityEngine;

namespace Entity.Bullet
{
    public class PlayerBullet : Bullet
    {
        protected override void OnTriggerEnter2D(Collider2D other)
        {
            base.OnTriggerEnter2D(other);
            if (other.CompareTag("Monster"))
            {
                var monster = other.GetComponent<Monster.Monster>();
                monster.TakeDamage(attackDamage);
                PoolManager.Instance.DestroyGameObject(gameObject, prefabType);
            }
            else if (other.CompareTag("Cell"))
            {
                var cell = other.GetComponent<Cell.Cell>();
                cell.Die();
                PoolManager.Instance.DestroyGameObject(gameObject, prefabType);
            }
        }
    }
}