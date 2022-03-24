using UnityEngine;

namespace Entity.Monster
{
    public class Virus : Monster
    {
        // default
        public override void Reset()
        {
            prefabType = PrefabType.Virus;
            maxHp = 100;
            CurrentHp = maxHp;
            attack = 10;
            moveSpeed = 1;
            attackSpeed = 1;
            bulletSpeed = 1;
            score = 10;
        }
    }
}