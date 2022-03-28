using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class CheatPanel : MonoBehaviour
    {
        [SerializeField] private Button stage1Button;
        [SerializeField] private Button stage2Button;

        [SerializeField] private Button weaponLevelDownButton;
        [SerializeField] private Button weaponLevelUpButton;
        [SerializeField] private Text weaponLevelText;

        [SerializeField] private Button nullityBuffButton;
        [SerializeField] private Text nullityBuffText;

        [SerializeField] private Slider playerHpSlider;
        [SerializeField] private Text playerHpText;

        [SerializeField] private Slider painSlider;
        [SerializeField] private Text painText;

        [SerializeField] private Button whiteCellSpawnButton;
        [SerializeField] private Button redCellSpawnButton;

        private void Awake()
        {
            stage1Button.onClick.AddListener(() => { SceneManager.LoadScene(1); });
            stage2Button.onClick.AddListener(() => { SceneManager.LoadScene(2); });
            weaponLevelDownButton.onClick.AddListener(() =>
            {
                GameManager.Instance.playerCharacter.WeaponLevel--;
                weaponLevelText.text = GameManager.Instance.playerCharacter.WeaponLevel.ToString();
            });
            weaponLevelUpButton.onClick.AddListener(() =>
            {
                GameManager.Instance.playerCharacter.WeaponLevel++;
                weaponLevelText.text = GameManager.Instance.playerCharacter.WeaponLevel.ToString();
            });

            nullityBuffButton.onClick.AddListener(() =>
            {
                var buff = !GameManager.Instance.playerCharacter.buffs[(int) PlayerBuff.Nullity];
                nullityBuffText.text = buff ? "유지" : "해체";
                GameManager.Instance.playerCharacter.buffs[(int) PlayerBuff.Nullity] = buff;
            });

            playerHpSlider.onValueChanged.AddListener(value =>
            {
                GameManager.Instance.playerCharacter.CurrentHp = (int) (GameManager.Instance.playerCharacter.maxHp * value);
                playerHpText.text = GameManager.Instance.playerCharacter.CurrentHp.ToString();
            });

            painSlider.onValueChanged.AddListener(value =>
            {
                GameManager.Instance.currentStage.CurrentPain = (int) (Stage.MAXPain * value);
                playerHpText.text = GameManager.Instance.currentStage.CurrentPain.ToString();
            });

            whiteCellSpawnButton.onClick.AddListener(() => { GameManager.Instance.currentStage.SpawnWhiteCell(); });
            redCellSpawnButton.onClick.AddListener(() => { GameManager.Instance.currentStage.SpawnRedCell(); });
        }
    }
}
