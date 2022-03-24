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
        [SerializeField] private Button deleteAllMonstersButton;

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
                GameManager.Instance.PlayerCharacter.WeaponLevel--;
                weaponLevelText.text = GameManager.Instance.PlayerCharacter.WeaponLevel.ToString();
            });
            weaponLevelUpButton.onClick.AddListener(() =>
            {
                GameManager.Instance.PlayerCharacter.WeaponLevel++;
                weaponLevelText.text = GameManager.Instance.PlayerCharacter.WeaponLevel.ToString();
            });
            weaponLevelText.text = GameManager.Instance.PlayerCharacter.WeaponLevel.ToString();

            nullityBuffButton.onClick.AddListener(() =>
            {
                var buff = !GameManager.Instance.PlayerCharacter.buffs[(int)PlayerBuff.Nullity];
                nullityBuffText.text = buff ? "유지" : "해체";
                GameManager.Instance.PlayerCharacter.buffs[(int)PlayerBuff.Nullity] = buff;
            });
            deleteAllMonstersButton.onClick.AddListener(() => { Stage.Instance.DeleteAllMonsters(); });

            playerHpSlider.onValueChanged.AddListener(value =>
            {
                GameManager.Instance.PlayerCharacter.CurrentHp =
                    (int)(GameManager.Instance.PlayerCharacter.maxHp * value);
                playerHpText.text = value.ToString("P0");
            });
            painSlider.onValueChanged.AddListener(value =>
            {
                Stage.Instance.CurrentPain = Stage.MAXPain * value;
                painText.text = value.ToString("P0");
            });
            playerHpText.text = playerHpSlider.value.ToString("P0");
            painText.text = painSlider.value.ToString("P0");

            whiteCellSpawnButton.onClick.AddListener(() => { Stage.Instance.Spawn(PrefabType.WhiteCell); });
            redCellSpawnButton.onClick.AddListener(() => { Stage.Instance.Spawn(PrefabType.RedCell); });
        }
    }
}