using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    public static T Instance
    {
        get
        {
            SingletonInit();
            return instance;
        }
    }

    private static void SingletonInit()
    {
        if (instance == null)
        {
            instance = FindObjectOfType<T>();
            if (instance == null)
            {
                instance = new GameObject(typeof(T).Name).AddComponent<T>();
                if (Application.isPlaying)
                    DontDestroyOnLoad(instance.gameObject);
            }
        }
    }
    
    private void Awake()
    {
        if (instance == this)
        {
            instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public virtual void Init()
    {
        Debug.Log($"Init {typeof(T).Name}");
    }
    
    public void SingletonDestory()
    {
        Destroy(instance.gameObject);
        instance = null;
    }
}
