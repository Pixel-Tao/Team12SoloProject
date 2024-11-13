using System;
using UnityEngine;

/// <summary>
/// 모든 씬에서 필수로 상속받아 사용
/// </summary>
public abstract class SceneBase : MonoBehaviour
{
    [SerializeField] private Defines.SceneType sceneType = Defines.SceneType.None;
    public Defines.SceneType SceneType => sceneType;
    
    private void Start()
    {
        OnSceneLoad();
        OnSceneLoaded();
    }
    
    private void OnDestroy()
    {
        OnSceneUnload();
    }

    /// <summary>
    /// 1. 씬 로드시 필요한 로직을 수행
    /// </summary>
    protected abstract void OnSceneLoad();
    /// <summary>
    /// 2. 씬 로드가 완료된 후 필요한 로직을 수행
    /// </summary>
    protected abstract void OnSceneLoaded();
    /// <summary>
    /// 3. 씬 언로드시 필요한 로직을 수행
    /// </summary>
    protected abstract void OnSceneUnload();

}