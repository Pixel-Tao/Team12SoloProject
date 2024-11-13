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
                instance = new GameObject($"@{typeof(T).Name}").AddComponent<T>();
                if (Application.isPlaying)
                    DontDestroyOnLoad(instance.gameObject);
            }
        }
    }

    public virtual void Init()
    {
        Debug.Log($"Init {typeof(T).Name}");
    }
    
    public virtual void Clear()
    {
        Debug.Log($"Clear {typeof(T).Name}");
    }
    
    public virtual void Release()
    {
        Destroy(instance.gameObject);
        instance = null;
    }
}
