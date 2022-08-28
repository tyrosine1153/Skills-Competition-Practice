using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class EndCanvas : Singleton<EndCanvas>
    {
        [SerializeField] private Button restartButton;
        [SerializeField] private Button mainButton;
        
        [SerializeField] private Transform cellContainer;
        [SerializeField] private RankCell rankCellPrefab;

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
        }

        public void UpdateScoreRankBoard()
        {
            var rankList = new List<(string playerName, int score)>();
            // Todo : rank 가져오기

            var cellList = cellContainer.GetComponentsInChildren<RankCell>();
            foreach (var cell in cellList)
            {
                Destroy(cell);
            }

            for (int i = 0; i < rankList.Count; i++)
            {
                Instantiate(rankCellPrefab, cellContainer).Set(i + 1, rankList[i].playerName, rankList[i].score);
            }
        }
    }
}