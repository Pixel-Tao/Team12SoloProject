using UnityEngine;
using UnityEngine.Pool;

public class ObjectPool
{
    private IObjectPool<GameObject> pool;

    private string name;
    private GameObject prefab;
    private Transform parent;

    public ObjectPool(string name, GameObject prefab, Transform parent = null, int defaultCapacity = 10, int maxSize = 100)
    {
        this.name = name;
        this.prefab = prefab;
        this.parent = parent;
        this.pool = new ObjectPool<GameObject>(
            createFunc: CreateObject,
            actionOnGet: GetObject,
            actionOnRelease: ReleaseObject,
            actionOnDestroy: DestoryObject,
            defaultCapacity: defaultCapacity,
            maxSize: maxSize
        );
    }

    public GameObject Spawn()
    {
        return pool.Get();
    }

    public void Despawn(GameObject go)
    {
        pool.Release(go);
    }

    private GameObject CreateObject()
    {
        GameObject go = GameObject.Instantiate(prefab, parent);
        go.name = name;
        return go;
    }

    private void GetObject(GameObject obj)
    {
        obj.gameObject.SetActive(true);
    }

    private void ReleaseObject(GameObject obj)
    {
        obj.gameObject.SetActive(false);
    }

    private void DestoryObject(GameObject obj)
    {
        GameObject.Destroy(obj);
    }
}