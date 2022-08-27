using System;
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

            foreach (var rank in rankList)
            {
                Instantiate(rankCellPrefab, cellContainer).Set(rank.playerName, rank.score);
            }
        }
    }
}