using System.Collections;
using UnityEngine;

public class GameScene : SceneBase
{
    protected override void OnSceneLoad()
    {
        // 1. 씬 로드시 필요한 로직을 수행
        Managers.UI.Init();
        Managers.Sound.Init();
        Managers.Pool.Init();
        Managers.Coroutine.Init();
        
        Managers.UI.LoadSceneUI<UIGameScene>();

        Managers.Coroutine.StartCoroutine("Dummy", Dummy());
    }

    private IEnumerator Dummy()
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject go = SpawnDummy(i);
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(DespawnDummy(go));
            yield return new WaitForSeconds(0.5f);
        }
    }

    private GameObject SpawnDummy(int num)
    {
        GameObject go = Managers.Pool.Spawn("Dummy");
        Vector3 pos = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
        go.transform.position = pos;
        SpriteRenderer sr = go.GetComponent<SpriteRenderer>();
        sr.sprite = Managers.Resource.Load<Sprite>($"Textures/status/status_{num}", true);
        return go;
    }

    private IEnumerator DespawnDummy(GameObject go)
    {
        yield return new WaitForSeconds(2f);
        Managers.Sound.PlaySFX(Random.Range(0,2) == 0 ? "Damaged" : "HitResource", go.transform.position);
        Managers.Pool.Despawn(go);
    }


    protected override void OnSceneLoaded()
    {
        // 2. 씬 로드가 완료된 후 필요한 로직을 수행
        Managers.Sound.PlayBGM("BGM");
        Managers.Sound.SetMasterVolume(0.3f);
        
        Managers.User.Earn(10_000_000_000);
    }
    protected override void OnSceneUnload()
    {
        // 3. 씬 언로드시 필요한 로직을 수행

    }
}