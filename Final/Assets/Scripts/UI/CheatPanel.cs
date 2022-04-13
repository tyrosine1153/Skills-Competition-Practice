using UnityEngine;
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
            stage1Button.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySfx(SfxClip.ButtonClick);
                GameManager.Instance.isGamePlaying = false;
                SceneManagerEx.LoadScene(SceneType.Stage1);
            });
            stage2Button.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySfx(SfxClip.ButtonClick);
                GameManager.Instance.isGamePlaying = false;
                SceneManagerEx.LoadScene(SceneType.Stage2);
            });
            weaponLevelDownButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySfx(SfxClip.ButtonClick);
                Stage.Instance.PlayerCharacter.BulletLevel--;
                weaponLevelText.text = Stage.Instance.PlayerCharacter.BulletLevel.ToString();
            });
            weaponLevelUpButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySfx(SfxClip.ButtonClick);
                Stage.Instance.PlayerCharacter.BulletLevel++;
                weaponLevelText.text = Stage.Instance.PlayerCharacter.BulletLevel.ToString();
            });
            weaponLevelText.text = Stage.Instance.PlayerCharacter.BulletLevel.ToString();

            nullityBuffButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySfx(SfxClip.ButtonClick);
                var buff = !Stage.Instance.PlayerCharacter.cheatNullityBuff;
                nullityBuffText.text = buff ? "유지" : "해제";
                Stage.Instance.PlayerCharacter.cheatNullityBuff = buff;
            });
            nullityBuffText.text = Stage.Instance.PlayerCharacter.cheatNullityBuff ? "유지" : "해제";
            deleteAllMonstersButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySfx(SfxClip.ButtonClick);
                Stage.Instance.KillAllMonsters();
            });

            playerHpSlider.onValueChanged.AddListener(value =>
            {
                Stage.Instance.PlayerCharacter.CurrentHp =
                    (int)(Stage.Instance.PlayerCharacter.maxHp * value);
                playerHpText.text = value.ToString("P0");
            });
            painSlider.onValueChanged.AddListener(value =>
            {
                Stage.Instance.CurrentPain = Stage.MaxPain * value;
                painText.text = value.ToString("P0");
            });
            playerHpText.text = playerHpSlider.value.ToString("P0");
            painText.text = painSlider.value.ToString("P0");

            whiteCellSpawnButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySfx(SfxClip.ButtonClick);
                Stage.Instance.Spawn(PrefabType.WhiteCell);
            });
            redCellSpawnButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySfx(SfxClip.ButtonClick);
                Stage.Instance.Spawn(PrefabType.RedCell);
            });
        }
    }
}