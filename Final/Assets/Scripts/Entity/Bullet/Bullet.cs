using UnityEngine;

namespace Entity.Bullet
{
    public abstract class Bullet : Entity
    {
        public Vector3 direction;
        public float attackDamage;
        
        protected override void Move()
        {
            transform.Translate(direction * moveSpeed * Time.deltaTime);
        }
    }
}