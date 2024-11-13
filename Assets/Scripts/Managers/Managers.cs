using System;
using UnityEngine;

public interface IManager
{
    void Init();
    void Clear();
}

public class Managers : MonoBehaviour
{

    #region Singleton

    private static Managers instance;
    public static Managers Instance
    {
        get
        {
            Initialize();
            return instance;
        }
    }

    private static bool initialized = false;
    private static long sequence = 0;
    private static object lockObj = new object();

    private static void Initialize()
    {
        if (instance == null && initialized == false)
        {
            initialized = true;
            sequence = DateTime.Now.Ticks;
            instance = FindObjectOfType<Managers>();
            if (instance == null)
            {
                instance = new GameObject($"@Managers").AddComponent<Managers>();
                DontDestroyOnLoad(instance.gameObject);
            }

            // Initialize all managers
            DB?.Init();
            Resource?.Init();
            Sound?.Init();
        }
    }

    #endregion

    #region CoreManagers

    private DBManager _db = new DBManager();
    private UIManager _ui = new UIManager();
    private SoundManager _sound = new SoundManager();
    private PoolManager _pool = new PoolManager();
    private ResourceManager _resource = new ResourceManager();
    private CoroutineManager _coroutine = new CoroutineManager();
    private SceneManagerEx _scene = new SceneManagerEx();

    public static DBManager DB
    {
        get { return Instance?._db; }
    }
    public static UIManager UI
    {
        get { return Instance?._ui; }
    }
    public static SoundManager Sound
    {
        get { return Instance?._sound; }
    }
    public static PoolManager Pool
    {
        get { return Instance?._pool; }
    }
    public static ResourceManager Resource
    {
        get { return Instance?._resource; }
    }
    public static CoroutineManager Coroutine
    {
        get { return Instance?._coroutine; }
    }
    public static SceneManagerEx Scene
    {
        get { return Instance?._scene; }
    }

    #endregion

    public static long GetNextSequence()
    {
        lock (lockObj)
            sequence = sequence + 1;

        return sequence;
    }
    

    public static void Clear()
    {
        UI?.Clear();
        Pool?.Clear();
        Sound?.Clear();
        Coroutine?.Clear();
    }

}