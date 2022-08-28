using UnityEngine.SceneManagement;

public enum SceneType
{
    Main,
    InGame,
    End,
}

public class SceneManagerEx : Singleton<SceneManagerEx>
{
    public SceneType CurrentSceneType
        => (SceneType)SceneManager.GetActiveScene().buildIndex;

    public void LoadScene(SceneType type)
    {
        SceneManager.LoadScene((int)type);
    }

    // Extend SceneManager
}