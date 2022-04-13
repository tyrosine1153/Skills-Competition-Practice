using System;
using UnityEngine;

namespace Entity
{
    public abstract class Entity : MonoBehaviour
    {
        public PrefabType prefabType;
        public float moveSpeed;

        protected virtual void Update()
        {
            Move();
        }

        protected virtual void Move()
        {
            transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
        }
        
        protected virtual void OnTriggerEnter2D(Collider2D other) 
        {
            if (other.CompareTag("Wall"))
            {
                PoolManager.Instance.DestroyGameObject(gameObject, prefabType);
            }
        }
    }
}
