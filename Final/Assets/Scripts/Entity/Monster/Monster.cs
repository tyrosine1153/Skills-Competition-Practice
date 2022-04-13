using System.Collections;
using UnityEngine;

namespace Entity.Monster
{
    public abstract class Monster : Battler
    {
        public int score;

        protected override void Start()
        {
            base.Start();
            StartCoroutine(CoAttack());
        }

        protected override void OnTriggerEnter2D(Collider2D other) 
        {
            if (other.CompareTag("Wall"))
            {
                Stage.Instance.Hurt(attackDamage * 0.5f);
                PoolManager.Instance.DestroyGameObject(gameObject, prefabType);
            }
        }
        
        protected virtual IEnumerator CoAttack()
        {
            while (true)
            {
                yield return new WaitForSeconds(attackSpeed);
                Attack();
            }
        }

        public override void Attack()
        {
            AudioManager.Instance.PlaySfx(SfxClip.EnemyAttack);

            var go = PoolManager.Instance.CreateGameObject(PrefabType.MonsterBullet);
            go.transform.position = transform.position;
            go.transform.rotation = Quaternion.identity;
            var bullet = go.GetComponent<Bullet.Bullet>();
            bullet.attackDamage = attackDamage;
            bullet.direction = Vector3.down;
            bullet.moveSpeed = bulletSpeed;
        }

        public override void Die()
        {
            GameManager.Instance.AddScore(score);
            PoolManager.Instance.DestroyGameObject(gameObject, prefabType);
        }
    }
}