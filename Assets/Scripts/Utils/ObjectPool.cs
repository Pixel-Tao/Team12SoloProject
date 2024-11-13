using UnityEngine;
using UnityEngine.Pool;

public class ObjectPool
{
    private IObjectPool<GameObject> pool;

    private string name;
    private GameObject prefab;
    private Transform parent;
    private Transform root
    {
        get
        {
            if (parent == null)
            {
                parent = new GameObject { name = $"@{prefab.name}_Pool" }.transform;
            }

            return parent;
        }
    }

    public ObjectPool(GameObject prefab, Transform parent = null, int defaultCapacity = 10, int maxSize = 100)
    {
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

    public GameObject Pop()
    {
        return pool.Get();
    }

    public void Push(GameObject go)
    {
        pool.Release(go);
    }
    
    public void Clear()
    {
        pool.Clear();
    }

    private GameObject CreateObject()
    {
        GameObject go = GameObject.Instantiate(prefab, root);
        go.name = prefab.name;
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