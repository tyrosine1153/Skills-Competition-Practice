using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class InGameCanvas : Singleton<InGameCanvas>
    {
        [SerializeField] private Text scoreText;
        [SerializeField] private Text stageText;
        [SerializeField] private Slider painGauge;
        [SerializeField] private GameObject cheatPanel;

        private void Start()
        {
            scoreText.text = "Score : " + GameManager.Instance.score;
            stageText.text = "Stage " + GameManager.Instance.currentStageNum;
        }

        public void SwitchCheatPanel()
        {
            cheatPanel.SetActive(!cheatPanel.activeSelf);
        }
    }
}