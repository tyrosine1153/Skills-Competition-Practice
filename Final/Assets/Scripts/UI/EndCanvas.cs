using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class EndCanvas : MonoBehaviour
    {
        public Text gameEndingText;
        public Transform rankingContents;
        public GameObject recordPrefab;

        public Text gameScoreText;
        public InputField playerName;
        public Button entryButton;
        public Button retryButton;
        public Button mainButton;

        private const int ShownRankingCount = 5;

        private void Awake()
        {
            gameEndingText.text = GameManager.Instance.GameEnding switch
            {
                EndingType.Clear => "Game Clear!",
                EndingType.Over => "Game Over!",
                _ => "Error"
            };
            UpdateRankingBoard();

            gameScoreText.text = GameManager.Instance.Score.ToString("N0");
            entryButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySfx(SfxClip.ButtonClick);
                GameManager.Instance.ScoreRanking.Add((playerName.text, GameManager.Instance.Score));
                UpdateRankingBoard();
                entryButton.interactable = false;
            });
            retryButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySfx(SfxClip.ButtonClick);
                SceneManagerEx.LoadScene(SceneType.Stage1);
            });
            mainButton.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlaySfx(SfxClip.ButtonClick);
                SceneManagerEx.LoadScene(SceneType.Main);
            });
            // Todo
        }

        private void UpdateRankingBoard()
        {
            var records = rankingContents.GetComponentsInChildren<Record>();
            foreach (var record in records)
            {
                Destroy(record.gameObject);
            }

            var scoreRanking = GameManager.Instance.ScoreRanking;
            var rankingCount = Mathf.Min(scoreRanking.Count, ShownRankingCount);
            for (int i = 0; i < rankingCount; i++)
            {
                var recordUI = Instantiate(recordPrefab, rankingContents).GetComponent<Record>();
                recordUI.recordIndexText.text = (i + 1).ToString();
                recordUI.recordScoreText.text = $"{scoreRanking[i].Item1} - {scoreRanking[i].Item2:N0}";
            }
        }
    }
}