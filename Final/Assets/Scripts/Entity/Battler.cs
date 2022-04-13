using UnityEngine;

namespace Entity
{
    public abstract class Battler : Entity
    {
        public float maxHp;
        protected float _currentHp;

        public virtual float CurrentHp
        {
            get => _currentHp;
            set
            {
                _currentHp = Mathf.Min(value, maxHp);
                if (_currentHp <= 0)
                {
                    Die();
                }
            }
        }

        public float attackDamage;
        public float attackSpeed;
        public float bulletSpeed;

        protected virtual void Start()
        {
            _currentHp = maxHp;
        }
        
        public virtual   void TakeDamage(float amount)
        {
            CurrentHp -= amount;
        }

        public abstract void Attack();

        public abstract void Die();
    }
}