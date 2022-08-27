using System;
using TMPro;
using UnityEngine;

namespace UI
{
    public class InGameCanvas : Singleton<InGameCanvas>
    {
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI playTimeText;
        [SerializeField] private TextMeshProUGUI hpText;
        [SerializeField] private TextMeshProUGUI[] inputCountTexts;

        public int Score
        {
            set => scoreText.text = $"Score : {value:N0}";
        }
        
        public int PlayTime
        {
            set
            {
                var t = TimeSpan.FromSeconds(value);
                playTimeText.text = $"Play Time : {t.Minutes} : {t.Seconds}";
            }
        }
        
        public int Hp
        {
            set => hpText.text = $"HP : {value}";
        }
        
        public void SetInputCount(KeyCode key, int count)
        {
            inputCountTexts[(int)key].text = $"{count}";
        }
    }
}