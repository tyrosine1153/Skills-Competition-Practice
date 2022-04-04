using System;
using UnityEngine;

public class BackgroundsMove : MonoBehaviour
{
    [Serializable]
    public struct Background
    {
        public Transform transform;
        public float moveSpeed;
    }

    [SerializeField] public Background[] backgrounds;
    private const float EndPoint = 6.5f;
    
    private void Update()
    {
        foreach (var background in backgrounds)
        {
            background.transform.Translate(Vector3.down * background.moveSpeed * Time.deltaTime);
            if (background.transform.position.y <= -EndPoint)
            {
                background.transform.Translate(Vector3.up * EndPoint * 2);
            }
        }
    }
}
