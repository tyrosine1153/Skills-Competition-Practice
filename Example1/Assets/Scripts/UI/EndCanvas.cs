using System;
using UnityEngine;
using UnityEngine.UI;

public class EndCanvas : MonoBehaviour
{
    [SerializeField] private Transform rankingContents;
    [SerializeField] private GameObject recordPrefab;

    [SerializeField] private Text gameScoreText;
    [SerializeField] private InputField playerName;
    [SerializeField] private Button entryButton;
    [SerializeField] private Button retryButton;
    [SerializeField] private Button mainButton;

    private void Awake()
    {
        UpdateRankingBoard();

        gameScoreText.text = GameManager.Instance.Score.ToString("N0");
        entryButton.onClick.AddListener(() =>
        {
            GameManager.Instance.ScoreRanking.Add((playerName.text, GameManager.Instance.Score));
            UpdateRankingBoard();
            entryButton.interactable = false;
        });
        retryButton.onClick.AddListener(() => { SceneManagerEx.MoveScene(SceneType.Stage1); });
        mainButton.onClick.AddListener(() => { SceneManagerEx.MoveScene(SceneType.Main); });
    }

    private void UpdateRankingBoard()
    {
        var records = rankingContents.GetComponentsInChildren<Record>();
        foreach (var record in records)
        {
            Destroy(record.gameObject);
        }

        var scoreRanking = GameManager.Instance.ScoreRanking;
        for (int i = 0; i < scoreRanking.Count; i++)
        {
            var recordUI = Instantiate(recordPrefab, rankingContents).GetComponent<Record>();
            recordUI.recordIndexText.text = (i + 1).ToString();
            recordUI.recordScoreText.text = $"{scoreRanking[i].Item1} - {scoreRanking[i].Item2:N0}";
        }
    }
}