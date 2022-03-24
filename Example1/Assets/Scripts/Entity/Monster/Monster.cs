using System.Collections;
using UnityEngine;

namespace Entity.Monster
{
    public abstract class Monster : Battler
    {
        public int score;

        protected void Start()
        {
            StartCoroutine(CoAttack());
        }

        public abstract void Reset();

        private void OnEnable()
        {
            CurrentHp = maxHp;
        }

        protected override void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Wall"))
            {
                if (other.gameObject.layer == LayerMask.NameToLayer("AddPain"))
                {
                    Stage.Instance.Hurt(attack * 0.5f);
                }

                PoolManager.Instance.DestroyGameObject(gameObject, prefabType);
            }
        }

        public override void Die()
        {
            GameManager.Instance.AddScore(score);
            PoolManager.Instance.DestroyGameObject(gameObject, prefabType);
        }

        public override void Move()
        {
            transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
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
            var bulletGO = PoolManager.Instance.CreateGameObject(PrefabType.MonsterBullet);
            bulletGO.transform.position = transform.position;
            bulletGO.transform.rotation = Quaternion.identity;
            var bullet = bulletGO.GetComponent<Bullet>();
            bullet.damage = attack;
            bullet.direction = Vector3.down;
            bullet.speed = bulletSpeed;
        }
    }
}