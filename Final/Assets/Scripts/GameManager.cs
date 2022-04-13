using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public int CurrentStageNum { get; private set; }
    public bool isGamePlaying;

    private int _score;
    public int Score
    {
        get => _score;
        private set
        {
            _score = value;
            StageCanvas.Instance.scoreText.text = _score.ToString("N0");
        }
    }
    public void AddScore(int amount)
    {
        if (amount < 0) return;
        Score += amount;
    }

    private readonly List<(string, int)> _scoreRanking = new List<(string, int)>();
    public List<(string, int)> ScoreRanking
    {
        get
        {
            _scoreRanking.Sort((r1, r2) => r2.Item2.CompareTo(r1.Item2));
            return _scoreRanking;
        }
    }

    private EndingType _gameEnding;

    public EndingType GameEnding { get; private set; }

    public ItemInfo[] itemInfos;

    public void GameStart()
    {
        if(isGamePlaying) return;
        isGamePlaying = true;
        StartCoroutine(CoGameStart());
    }

    private IEnumerator CoGameStart()
    {
        CurrentStageNum = Stage.Instance.stageNum;
        if (CurrentStageNum == 1)
        {
            AudioManager.Instance.PlaySfx(SfxClip.GameStart);
            _score = 0;
            GameEnding = EndingType.None;
        }

        yield return new WaitForSeconds(3f);
        StageCanvas.Instance.StageStart();
        
        yield return new WaitForSeconds(1f);
        Stage.Instance.PlayerCharacter.canMove = true;
        Stage.Instance.StageStart();
    }

    public void GameEnd(bool isGameClear)
    {
        if (!isGamePlaying) return;
        isGamePlaying = false;
        GameEnding = isGameClear ? EndingType.Clear : EndingType.Over;
        StartCoroutine(CoGameEnd(isGameClear));
    }

    private IEnumerator CoGameEnd(bool isGameClear)
    {
        yield return new WaitForSeconds(0.5f);
        Stage.Instance.StageEnd();
        Stage.Instance.PlayerCharacter.canMove = false;
        AudioManager.Instance.StopBgm();
        yield return new WaitForSeconds(2f);
        StageCanvas.Instance.BonusScore(Stage.Instance.PlayerCharacter.CurrentHp,
            Stage.MaxPain - Stage.Instance.CurrentPain, Stage.Instance.PlayerCharacter.ItemGetCount);
        yield return new WaitForSeconds(10f);

        if (isGameClear)
        {
            SceneManagerEx.LoadNextScene();
        }
        else
        {
            SceneManagerEx.LoadScene(SceneType.End);
        }
    }
}
