using UnityEngine;

public class Bullet : MonoBehaviour
{
    public PrefabType prefabType;
    public float damage = 1;
    public float speed = 1;
    public Vector3 direction;

    private void Reset()
    {
        direction = Vector3.up;
    }

    private void Update()
    {
        Move();
    }

    protected virtual void Move()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            PoolManager.Instance.DestroyGameObject(gameObject, prefabType);
        }
    }
}