using System;
using UnityEngine;

public class Stage : MonoBehaviour
{
    public int startPain;
    public int currentPain;
    private const int MAXPain = 100;
    private const int MINPain = 0;

    private void Start()
    {
        currentPain = startPain;
    }

    public void Heal(int amount)
    {
        currentPain = Math.Max(currentPain - amount, MINPain);
        // UI
    }
    
    public void Hurt(int amount)
    {
        currentPain = Math.Min(currentPain + amount, MAXPain);
        if(currentPain >= MAXPain)
        {
            GameManager.Instance.GameOver();
        }
        // UI
    }
}
