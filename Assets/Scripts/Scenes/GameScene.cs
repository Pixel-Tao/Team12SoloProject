using System.Collections;
using UnityEngine;

public class GameScene : SceneBase
{
    protected override void OnSceneLoad()
    {
        // 1. 씬 로드시 필요한 로직을 수행
        DBManager.Instance.Init();
        ResourceManager.Instance.Init();

        StartCoroutine(Dummy());
    }

    private IEnumerator Dummy()
    {
        GameObject poolGo = new GameObject("Pooling");
        for (int i = 0; i < 10; i++)
        {
            GameObject go = SpawnDummy(poolGo.transform, i);
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(DespawnDummy(go));
            yield return new WaitForSeconds(0.5f);
        }
    }

    private GameObject SpawnDummy(Transform parent, int num)
    {
        GameObject go = PoolManager.Instance.Spawn("Dummy", parent);
        Vector3 pos = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
        go.transform.position = pos;
        SpriteRenderer sr = go.GetComponent<SpriteRenderer>();
        sr.sprite = ResourceManager.Instance.Load<Sprite>($"Textures/status/status_{num}", true);
        return go;
    }

    private IEnumerator DespawnDummy(GameObject go)
    {
        yield return new WaitForSeconds(2f);
        PoolManager.Instance.Despawn(go);
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