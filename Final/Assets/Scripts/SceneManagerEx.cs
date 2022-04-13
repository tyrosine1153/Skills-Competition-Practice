using UnityEngine.SceneManagement;

public class SceneManagerEx : Singleton<SceneManagerEx>
{
    public static void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    } 
    
    public static void LoadScene(SceneType sceneType)
    {
        SceneManager.LoadScene((int) sceneType);
    }
}
