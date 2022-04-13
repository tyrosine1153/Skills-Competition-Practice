using System.Collections;
using UnityEngine;

namespace Entity.Monster.BossMonster
{
    public class CoronaVirusVariety : BossMonster
    {
        protected override void OnEnable()
        {
            base.OnEnable();
            AudioManager.Instance.PlayBgm(BgmClip.Stage2Boss);
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
        
        public override void Pattern1(float rotation)
        {
            for (var i = 0; i < 40; i++)
            {
                var go = PoolManager.Instance.CreateGameObject(PrefabType.MonsterBullet);
                go.transform.position = transform.position;
                go.transform.rotation = Quaternion.identity;
                go.transform.Rotate(new Vector3(0, 0, i * 9 + rotation));
                var bullet = go.GetComponent<Bullet.Bullet>();
                bullet.attackDamage = attackDamage;
                bullet.direction = Vector3.down;
                bullet.moveSpeed = bulletSpeed;
            }
        }

        public override void Pattern2(float rotation)
        {
            Pattern1(rotation);
        }

        public override void Pattern3()
        {
            Stage.Instance.Spawn(PrefabType.Cancer);
        }
    }
}