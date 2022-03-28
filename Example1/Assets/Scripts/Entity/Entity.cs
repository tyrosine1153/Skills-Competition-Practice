using UnityEngine;

namespace Entity
{
    public abstract class Entity : MonoBehaviour
    {
        public int maxHp;
        protected int currentHp;
        public int CurrentHp
        {
            get => currentHp;
            set
            {
                currentHp = Mathf.Min(value, maxHp);
                if (currentHp <= 0)
                {
                    currentHp = 0;
                    Die();
                }
            }
        }
        
        public abstract void Move();
        public abstract void Die();
        public abstract void TakeDamage(int damage);
    }
}
