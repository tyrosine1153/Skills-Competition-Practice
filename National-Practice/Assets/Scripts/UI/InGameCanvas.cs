using System;
using TMPro;
using UnityEngine;

namespace UI
{
    public class InGameCanvas : Singleton<InGameCanvas>
    {
        [SerializeField] private TextMeshProUGUI stageIdText;
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI playTimeText;
        [SerializeField] private TextMeshProUGUI hpText;
        [SerializeField] private TextMeshProUGUI[] inputCountTexts;

        public int StageId
        {
            set => stageIdText.text = $"{value} Stage";
        }
        
        public int Score
        {
            set => scoreText.text = $"Score : {value:N0}";
        }
        
        public int PlayTime
        {
            set
            {
                var t = TimeSpan.FromSeconds(value);
                playTimeText.text = $"Play Time\n{t.Minutes:D2} : {t.Seconds:D2}";
            }
        }
        
        public int Hp
        {
            set => hpText.text = $"HP : {value}";
        }
        
        public void SetInputCount(int key, int count)
        {
            inputCountTexts[key].text = $"{count}";
        }
    }
}