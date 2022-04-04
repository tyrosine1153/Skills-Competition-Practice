using System;
using System.Collections;
using System.Collections.Generic;
using Entity;
using UI;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private PlayerCharacter _playerCharacter;

    public PlayerCharacter PlayerCharacter
    {
        get
        {
            if (_playerCharacter == null)
            {
                _playerCharacter = FindObjectOfType<PlayerCharacter>();
            }

            return _playerCharacter;
        }
        private set => _playerCharacter = value;
    }

    public int CurrentStageNum { get; private set; }

    private int _score;

    public int Score
    {
        get => _score;
        private set
        {
            _score = value;
            StageCanvas.Instance.scoreText.text = "Score : " + _score.ToString("N0");
        }
    }

    public void AddScore(int score)
    {
        Score += score;
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

    #region GameFlow

    private bool _isGamePlaying = false;
    public void GameStart()
    {
        
        StartCoroutine(CoGameStart());
    }

    private IEnumerator CoGameStart()
    {
        Debug.Log("Game Start");
        CurrentStageNum = Stage.Instance.stageNum;
        if (CurrentStageNum == 1)
        {
            _score = 0;
        }
        if (_isGamePlaying) yield break;
        _isGamePlaying = true;

        yield return new WaitForSeconds(3f);
        StageCanvas.Instance.OnGameStart();

        yield return new WaitForSeconds(1f);
        _playerCharacter.isGamePlaying = true;
        Stage.Instance.OnGameStart();
    }

    private void GameEnd(bool isGameClear)
    {
        StartCoroutine(CoGameEnd(isGameClear));
    }

    private IEnumerator CoGameEnd(bool isGameClear)
    {
        if (!_isGamePlaying) yield break;
        _isGamePlaying = false;
        
        yield return new WaitForSeconds(0.5f);
        Stage.Instance.EndStage();
        _playerCharacter.isGamePlaying = false;
        yield return new WaitForSeconds(2f);
        // ToDo : 점수 추가정산

        if (isGameClear)
        {
            SceneManagerEx.Instance.MoveToNextScene();
        }
        else
        {
            SceneManagerEx.MoveScene(SceneType.End);
        }
    }

    public void GameOver()
    {
        if (Stage.Instance.stageNum != CurrentStageNum)
        {
            return;
        }

        GameEnd(false);
    }

    public void GameClear()
    {
        // 체력, 고통 게이지, 얻은 아이템 따라서 보너스 스코어 추가 정산
        if (Stage.Instance.stageNum != CurrentStageNum)
        {
            return;
        }

        GameEnd(true);
    }

    #endregion
}