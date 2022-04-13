using UnityEngine;

namespace Entity.Monster
{
    public class Cancer : Monster
    {
        private const int BulletLevel = 3;
        public override void Attack()
        {
            AudioManager.Instance.PlaySfx(SfxClip.EnemyAttack);

            const float n = 0.125f;
            const int d = 2;
            var position = 1 - BulletLevel;

            for (var i = 0; i < BulletLevel; i++)
            {
                var setPosition = new Vector3(position * n, 0, 0);
                var go = PoolManager.Instance.CreateGameObject(PrefabType.MonsterBullet);
                go.transform.position = transform.position + setPosition;
                go.transform.rotation = Quaternion.identity;
                var bullet = go.GetComponent<Bullet.Bullet>();
                bullet.attackDamage = attackDamage;
                bullet.direction = Vector3.down;
                bullet.moveSpeed = bulletSpeed;

                position += d;
            }
        }
    }
}