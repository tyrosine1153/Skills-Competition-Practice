using UnityEngine;

namespace Entity.Bullet
{
    public class MonsterBullet : Bullet
    {
        protected override void OnTriggerEnter2D(Collider2D other)
        {
            base.OnTriggerEnter2D(other);
            if (other.CompareTag("Player"))
            {
                var player = other.GetComponent<PlayerCharacter>();
                player.TakeDamage(attackDamage);
                
                PoolManager.Instance.DestroyGameObject(gameObject, prefabType);
            }
        }
    }
}