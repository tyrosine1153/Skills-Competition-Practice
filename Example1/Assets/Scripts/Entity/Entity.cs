using UnityEngine;

namespace Entity
{
    public abstract class Entity : MonoBehaviour
    {
        public PrefabType prefabType;

        public float maxHp;
        protected float currentHp;
        public float moveSpeed;

        public virtual float CurrentHp
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

        protected virtual void Update()
        {
            Move();
        }

        public abstract void Move();
        public abstract void Die();

        public virtual void TakeDamage(float damage)
        {
            CurrentHp -= damage;
        }

        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Wall"))
            {
                PoolManager.Instance.DestroyGameObject(gameObject, prefabType);
            }
        }
    }
}