using System;
using UnityEngine;

namespace UI
{
    public class BackgroundsMoveUI : MonoBehaviour
    {
        [Serializable]
        public struct BackgroundUI
        {
            public RectTransform transform;
            public float moveSpeed;
        }

        [SerializeField] public BackgroundUI[] backgrounds;
        private const float EndPoint = 960f;
        private const float Ratio = 100f;
    
        private void Update()
        {
            foreach (var background in backgrounds)
            {
                background.transform.Translate(Vector3.down * background.moveSpeed * Time.deltaTime * Ratio);
                if (background.transform.anchoredPosition.y <= -EndPoint)
                {
                    background.transform.Translate(Vector3.up * EndPoint * 2);
                    
                }
            }
        }
    }
}
