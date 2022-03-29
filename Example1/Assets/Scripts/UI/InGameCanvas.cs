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
            scoreText.text = "Score : " + GameManager.Instance.Score;
            stageText.text = "Stage " + GameManager.Instance.CurrentStageNum;
            painGauge.value = (float)GameManager.Instance.CurrentStage.CurrentPain / Stage.MAXPain;
        }

        public void SwitchCheatPanel()
        {
            cheatPanel.SetActive(!cheatPanel.activeSelf);
        }
    }
}