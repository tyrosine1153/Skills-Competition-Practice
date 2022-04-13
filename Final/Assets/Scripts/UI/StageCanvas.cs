using System.Collections;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class StageCanvas : Singleton<StageCanvas>
    {
        public Text scoreText;
        public Text scoreDescriptionText;
        public Text stageText;
        public Slider healthGauge;
        public Slider bossGauge;
        public Slider painGauge;
        public Text healthText;
        public Text bossText;
        public Text painText;
        public GameObject cheatPanel;
        public GameObject startPanel;
        public Animator endPanel;
        public Image[] weaponLevelTiles;

        public Image itemImage;
        public Text itemName;
        public Text itemDescription;

        public void StageStart()
        {
            healthGauge.onValueChanged.AddListener(_ =>
            {
                healthText.text = Stage.Instance.PlayerCharacter.CurrentHp.ToString("N0");
            });
            painGauge.onValueChanged.AddListener(_ =>
            {
                painText.text = Stage.Instance.CurrentPain.ToString("N0");
            });

            scoreText.text = GameManager.Instance.Score.ToString("N0");
            scoreDescriptionText.text = "";
            stageText.text = "Stage " + GameManager.Instance.CurrentStageNum;
            healthGauge.value = Stage.Instance.PlayerCharacter.CurrentHp
                                / Stage.Instance.PlayerCharacter.maxHp;
            painGauge.value = Stage.Instance.CurrentPain / Stage.MaxPain;
            bossGauge.gameObject.SetActive(false);
            startPanel.SetActive(false);

            healthText.text = Stage.Instance.PlayerCharacter.CurrentHp.ToString("N0");
            painText.text = Stage.Instance.CurrentPain.ToString("N0");

            itemImage.sprite = null;
            itemImage.color = Color.clear;
            itemName.text = "";
            itemDescription.text = "";
        }

        public void SwitchCheatPanel()
        {
            cheatPanel.SetActive(!cheatPanel.activeSelf);
        }

        public void OnBulletLevelChanged(int level)
        {
            for (int i = 0; i < weaponLevelTiles.Length; i++)
            {
                weaponLevelTiles[i].color = i < level ? Color.blue : Color.gray;
            }
        }

        private bool _isItemOn;
        private Coroutine _itemCoroutine;
        public void OnGetItem(ItemType itemType)
        {
            if (_isItemOn)
            {
                StopCoroutine(_itemCoroutine);
            }

            _itemCoroutine = StartCoroutine(CoOnGetItem(itemType));
        }

        private IEnumerator CoOnGetItem(ItemType itemType)
        {
            _isItemOn = true;
            itemImage.sprite = GameManager.Instance.itemInfos[(int) itemType].sprite;
            itemImage.color = Color.white;
            itemName.text = GameManager.Instance.itemInfos[(int) itemType].name;
            itemDescription.text = GameManager.Instance.itemInfos[(int) itemType].description;

            yield return new WaitForSeconds(5f);

            itemImage.sprite = null;
            itemImage.color = Color.clear;
            itemName.text = "";
            itemDescription.text = "";
            _isItemOn = false;
        }

        public void BonusScore(float playerHp, float bodyPain, int itemGetCount)
        {
            StartCoroutine(CoBonusScore(playerHp, bodyPain, itemGetCount));
        }

        private IEnumerator CoBonusScore(float playerHp, float bodyPain, int itemGetCount)
        {
            AudioManager.Instance.PlaySfx(SfxClip.BonusScoreCalc);
            scoreText.color = Color.white;
            scoreDescriptionText.color = Color.blue;
            scoreDescriptionText.text = $"+ [남은 백신 체력] {(int) playerHp} * 5 = {(int) playerHp * 5}";
            yield return new WaitForSeconds(1f);
            
            AudioManager.Instance.PlaySfx(SfxClip.BonusScoreGet);
            scoreText.color = Color.blue;
            GameManager.Instance.AddScore((int) playerHp * 5);
            yield return new WaitForSeconds(2f);
            
            AudioManager.Instance.PlaySfx(SfxClip.BonusScoreCalc);
            scoreText.color = Color.white;
            scoreDescriptionText.color = Color.red;
            scoreDescriptionText.text = $"+ [남은 고통 수치] {(int) bodyPain} * 5 = {(int) bodyPain * 5}";
            yield return new WaitForSeconds(1f);
            
            AudioManager.Instance.PlaySfx(SfxClip.BonusScoreGet);
            scoreText.color = Color.red;
            GameManager.Instance.AddScore((int) bodyPain * 5);
            yield return new WaitForSeconds(2f);
            
            AudioManager.Instance.PlaySfx(SfxClip.BonusScoreCalc);
            scoreText.color = Color.white;
            scoreDescriptionText.color = Color.yellow;
            scoreDescriptionText.text = $"+ [획득한 아이템 수] {itemGetCount} * 50 = {itemGetCount * 50}";
            yield return new WaitForSeconds(1f);
            
            AudioManager.Instance.PlaySfx(SfxClip.BonusScoreGet);
            scoreText.color = Color.yellow;
            GameManager.Instance.AddScore(itemGetCount * 50);
            yield return new WaitForSeconds(3f);
        }
    }
}