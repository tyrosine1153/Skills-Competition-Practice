using System;
using UnityEngine;
using UnityEngine.UI;

public class MainCanvas : MonoBehaviour
{
    public Button startButton;

    private void Start()
    {
        try
        {

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        startButton.onClick.AddListener(() =>
        {
            SceneManagerEx.Instance.MoveToNextScene();
        });
    }
}