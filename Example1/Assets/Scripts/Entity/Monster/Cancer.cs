using System;
using UnityEngine;

namespace Entity.Monster
{
    public class Cancer : Monster
    {
        // default
        public override void Reset()
        {
            prefabType = PrefabType.Cancer;
            maxHp = 100;
            CurrentHp = maxHp;
            attack = 10;
            moveSpeed = 1;
            attackSpeed = 1;
            bulletSpeed = 1;
            score = 10;
        }

        public override void Attack()
        {
            const int bulletCount = 3;
            const float n = 0.125f; // 단위 일반화 변수
            const int d = 2; // 위치의 공차
            var position = 1 - bulletCount; // 초기 위치

            for (var i = 0; i < bulletCount; i++)
            {
                var setPosition = new Vector3(position * n, 0, 0);
                var bulletGO = PoolManager.Instance.CreateGameObject(PrefabType.MonsterBullet);
                bulletGO.transform.position = transform.position + setPosition;
                bulletGO.transform.rotation = Quaternion.identity;
                var bullet = bulletGO.GetComponent<Bullet>();
                bullet.damage = attack;
                bullet.direction = Vector3.down;
                bullet.speed = bulletSpeed;

                position += d;
            }
        }
    }
}