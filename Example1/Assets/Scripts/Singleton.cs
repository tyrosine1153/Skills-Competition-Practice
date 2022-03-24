using UnityEngine;


[DisallowMultipleComponent]
public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    public bool dontDestroyOnLoad;

    private static volatile T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null) _instance = FindObjectOfType<T>();
            return _instance;
        }
    }

    protected virtual void Awake()
    {
        if (_instance != null)
        {
            Debug.LogError(GetType().Name + " Singleton class is already created.");
            Destroy(gameObject);
        }

        if (dontDestroyOnLoad)
        {
            DontDestroyOnLoad(this);
        }

        _instance = (T)this;
    }

    protected virtual void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }
}