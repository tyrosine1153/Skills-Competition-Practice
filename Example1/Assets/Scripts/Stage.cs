using System;
using UnityEngine;

public class Stage : MonoBehaviour
{
    #region Pains Managing

    public int startPain;
    private int currentPain;

    public int CurrentPain
    {
        get => currentPain;
        set
        {
            currentPain = Mathf.Max(value, MINPain);
            if (currentPain >= MAXPain)
            {
                currentPain = MAXPain;
                GameManager.Instance.GameOver();
            }
        }
    }

    public const int MAXPain = 100;
    private const int MINPain = 0;

    private void Start()
    {
        currentPain = startPain;
    }

    public void Heal(int amount)
    {
        currentPain -= amount;
        // UI
    }

    public void Hurt(int amount)
    {
        currentPain += amount;
        // UI
    }

    #endregion

    #region Objects Spawning
    
    public void SpawnWhiteCell()
    {
        
    }
    public void SpawnRedCell()
    {
        
    }

    public void DeleteAllMonsters()
    {
        
    }

    #endregion

    public void EndStage()
    {
        
    }
}
