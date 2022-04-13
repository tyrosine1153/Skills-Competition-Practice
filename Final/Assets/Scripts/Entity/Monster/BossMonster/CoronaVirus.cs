using System.Collections;
using UnityEngine;

namespace Entity.Monster.BossMonster
{
    public class CoronaVirus : BossMonster
    {
        protected override void OnEnable()
        {
            base.OnEnable();
            AudioManager.Instance.PlayBgm(BgmClip.Stage1Boss);
        }
        
        protected override IEnumerator CoAttack()
        {
            yield return new WaitForSeconds(0.2f);
            var rotation = 0;
            while (true)
            {
                var patternNumber = Random.Range(0, 3);
                AudioManager.Instance.PlaySfx(SfxClip.BossAttack);
                switch (patternNumber)
                {
                    case 0:
                        for (var i = 0; i < 5; i++)
                        {
                            Pattern1(i * 7.5f + rotation);
                            yield return new WaitForSeconds(2.5f);
                        }

                        break;
                    case 1:
                        for (var i = 0; i < 5; i++)
                        {
                            Pattern2(rotation);
                            yield return new WaitForSeconds(1f);
                        }

                        break;
                    case 2:
                        Pattern3();
                        yield return new WaitForSeconds(2);
                        break;
                }
                yield return new WaitForSeconds(2);
                rotation++;
                rotation %= 360;
            }
        }

        public override void Pattern1(float rotation)
        {
            for (var i = 0; i < 36; i++)
            {
                var go = PoolManager.Instance.CreateGameObject(PrefabType.MonsterBullet);
                go.transform.position = transform.position;
                go.transform.rotation = Quaternion.identity;
                go.transform.Rotate(new Vector3(0, 0, i * 10 + rotation));
                var bullet = go.GetComponent<Bullet.Bullet>();
                bullet.attackDamage = attackDamage;
                bullet.direction = Vector3.down;
                bullet.moveSpeed = bulletSpeed;
            }
        }

        public override void Pattern2(float rotation)
        {
            for (var i = 0; i < 36; i++)
            {
                var go = PoolManager.Instance.CreateGameObject(PrefabType.MonsterBullet);
                go.transform.position = transform.position;
                go.transform.rotation = Quaternion.identity;
                go.transform.Rotate(new Vector3(0, 0, i * 10 + rotation));
                var bullet = go.GetComponent<Bullet.Bullet>();
                bullet.attackDamage = attackDamage;
                bullet.direction = Vector3.down;
                bullet.moveSpeed = bulletSpeed;
            }
        }

        public override void Pattern3()
        {
            for (var i = 0; i < 3; i++)
            {
                Stage.Instance.Spawn(PrefabType.Virus);   
            }
        }
    }
}