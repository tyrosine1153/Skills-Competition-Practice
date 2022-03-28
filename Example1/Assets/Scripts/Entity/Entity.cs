using UnityEngine;

namespace Entity
{
    public abstract class Entity : MonoBehaviour
    {
        public int maxHp;
        public int hp;

        public abstract void Move();
        public abstract void Die();
        public abstract void TakeDamage(int damage);
    }
}