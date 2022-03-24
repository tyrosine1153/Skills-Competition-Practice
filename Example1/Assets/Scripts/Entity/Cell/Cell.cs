using UnityEngine;

namespace Entity.Cell
{
    public abstract class Cell : Entity
    {
        // default
        protected void Reset()
        {
            maxHp = 1;
            CurrentHp = maxHp;
            moveSpeed = 1;
        }

        public override void Move()
        {
            transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
        }

        public override void Die()
        {
            PoolManager.Instance.DestroyGameObject(gameObject, prefabType);
        }
    }
}
