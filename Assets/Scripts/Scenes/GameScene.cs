using UnityEngine;

public class GameScene : SceneBase
{
    protected override void OnSceneLoad()
    {
        // 1. 씬 로드시 필요한 로직을 수행
        DBManager.Instance.Init();
        ResourceManager.Instance.Init();

        GameObject go = ResourceManager.Instance.Instantiate("Prefabs/Dummy");
        go.transform.position = Vector3.zero;
        SpriteRenderer sr = go.GetComponent<SpriteRenderer>();
        sr.sprite = ResourceManager.Instance.Load("Textures/status/status_0", true);
    }
    protected override void OnSceneLoaded()
    {
        // 2. 씬 로드가 완료된 후 필요한 로직을 수행
        
    }
    protected override void OnSceneUnload()
    {
        // 3. 씬 언로드시 필요한 로직을 수행
        
    }
}