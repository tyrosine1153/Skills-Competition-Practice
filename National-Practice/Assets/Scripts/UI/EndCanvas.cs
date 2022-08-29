using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class EndCanvas : Singleton<EndCanvas>
    {
        [SerializeField] private Button restartButton;
        [SerializeField] private Button mainButton;

        [SerializeField] private TMP_InputField newRankSubmitInputField;
        [SerializeField] private Button newRankSubmitButton;
        
        [SerializeField] private Transform cellContainer;
        [SerializeField] private RankCell rankCellPrefab;

        private int cachedScore;

        private void Start()
        {
            restartButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySfx(SfxClip.ButtonClick);
                GameManager.Instance.SaveData(0);
                SceneManagerEx.Instance.LoadScene(SceneType.InGame);
            });

            mainButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySfx(SfxClip.ButtonClick);
                SceneManagerEx.Instance.LoadScene(SceneType.Main);   
            });

            newRankSubmitButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySfx(SfxClip.ButtonClick);
                GameManager.Instance.UpdateScoreRank(newRankSubmitInputField.text, cachedScore);
            });

            UpdateScoreRankBoard();
        }

        public void Set(bool isGameClear, int score)
        {
            // Todo : 텍스트 설정
            cachedScore = score;
            newRankSubmitInputField.gameObject.SetActive(isGameClear);
            newRankSubmitButton.gameObject.SetActive(isGameClear);
        }

        public void UpdateScoreRankBoard()
        {
            var rankList = GameManager.Instance.scoreRanking;

            var cellList = cellContainer.GetComponentsInChildren<RankCell>();
            foreach (var cell in cellList)
            {
                Destroy(cell.gameObject);
            }

            for (int i = 0; i < rankList.Count; i++)
            {
                Instantiate(rankCellPrefab, cellContainer).Set(i + 1, rankList[i].playerName, rankList[i].score);
            }
        }
    }
}