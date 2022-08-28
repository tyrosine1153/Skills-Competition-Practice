using TMPro;
using UnityEngine;

namespace UI
{
    public class RankCell : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI rankIndexText;
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI scoreText;
        
        public void Set(int rankIndex, string playerName, int score)
        {
            rankIndexText.text = rankIndex.ToString();
            nameText.text = playerName;
            scoreText.text = score.ToString("N0");
        }
    }
}