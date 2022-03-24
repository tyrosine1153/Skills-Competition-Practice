using System.Collections;
using UnityEngine;

namespace Entity.Monster
{
    public class CoronaVirusVariety : BossMonster
    {
        // default
        public override void Reset()
        {
            prefabType = PrefabType.CoronavirusVariety;
            maxHp = 100;
            CurrentHp = maxHp;
            attack = 10;
            moveSpeed = 2;
            attackSpeed = 2;
            bulletSpeed = 2;
            score = 10;
        }

        #region Action

        public override void Attack()
        {
        }
        
        protected override IEnumerator CoAttack()
        {
            yield return new WaitForSeconds(0.2f);
            var rotation = 0f;
            while (true)
            {
                var patternNumber = Random.Range(0, 3);
                switch (patternNumber)
                {
                    case 0:
                        for (var i = 0; i < 3; i++)
                        {
                            Pattern1(i * 3f + rotation);
                            yield return new WaitForSeconds(0.3f);
                        }

                        break;
                    case 1:
                        for (var i = 0; i < 5; i++)
                        {
                            rotation += 4.5f;
                            Pattern2(rotation);
                            yield return new WaitForSeconds(1f);
                        }

                        break;
                    case 2:
                        Pattern3();
                        break;
                }
                yield return new WaitForSeconds(2);
                rotation++;
                rotation %= 360;
            }
        }

        private void Pattern1(float rotation)
        {
            for (var i = 0; i < 40; i++)
            {
                var bulletGO = PoolManager.Instance.CreateGameObject(PrefabType.MonsterBullet);
                bulletGO.transform.position = transform.position;
                bulletGO.transform.rotation = Quaternion.identity;
                bulletGO.transform.Rotate(new Vector3(0, 0, i * 9 + rotation));
                var bullet = bulletGO.GetComponent<Bullet>();
                bullet.damage = attack;
                bullet.direction = Vector3.down;
                bullet.speed = bulletSpeed;
            }
        }
    
        private void Pattern2(float rotation)
        {
            for (var i = 0; i < 40; i++)
            {
                var bulletGO = PoolManager.Instance.CreateGameObject(PrefabType.MonsterBullet);
                bulletGO.transform.position = transform.position;
                bulletGO.transform.rotation = Quaternion.identity;
                bulletGO.transform.Rotate(new Vector3(0, 0, i * 9  + rotation));
                var bullet = bulletGO.GetComponent<Bullet>();
                bullet.damage = attack;
                bullet.direction = Vector3.down;
                bullet.speed = bulletSpeed;
            }
        }

        private void Pattern3()
        {
            for (var i = 0; i < 3; i++)
            {
                Stage.Instance.Spawn(PrefabType.Cancer);
            }
        }
        
        #endregion
    }
}