using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class StageCanvas : Singleton<StageCanvas>
    {
        public Text scoreText;
        public Text stageText;
        public Slider healthGauge;
        public Slider bossGauge;
        public Slider painGauge;
        public GameObject cheatPanel;
        public GameObject startPanel;
        public Animator endPanel;

        public void OnGameStart()
        {
            scoreText.text = "Score : " + GameManager.Instance.Score.ToString("N0");
            stageText.text = "Stage " + GameManager.Instance.CurrentStageNum;
            painGauge.value = Stage.Instance.CurrentPain / Stage.MAXPain;

            healthGauge.value = GameManager.Instance.PlayerCharacter.CurrentHp
                                / GameManager.Instance.PlayerCharacter.maxHp;
            bossGauge.gameObject.SetActive(false);
            startPanel.SetActive(false);
        }

        public void SwitchCheatPanel()
        {
            cheatPanel.SetActive(!cheatPanel.activeSelf);
        }
    }
}