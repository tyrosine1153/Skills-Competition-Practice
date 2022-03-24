using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneType
{
    Main,
    Stage1,
    Stage2,
    End
}

public class SceneManagerEx : Singleton<SceneManagerEx>
{
    [ContextMenu("MoveToNextScene")]
    public void MoveToNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public static void MoveScene(SceneType sceneType)
    {
        SceneManager.LoadScene((int)sceneType);
    }
}