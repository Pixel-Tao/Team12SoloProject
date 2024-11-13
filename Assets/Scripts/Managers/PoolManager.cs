using System.Collections.Generic;
using UnityEngine;

public class PoolManager : Singleton<PoolManager>
{
    private Dictionary<string, ObjectPool> poolDict = new Dictionary<string, ObjectPool>();

    private int defaultCapacity = 10;
    private int maxSize = 100;

    public override void Init()
    {
        
    }

    private ObjectPool CreatePool(string prefabPathWithoutPrefabRoot, Transform parent = null)
    {
        GameObject prefab = ResourceManager.Instance.Load<GameObject>($"Prefabs/{prefabPathWithoutPrefabRoot}");
        if (prefab == null)
        {
            Debug.Log($"Failed to load prefab : {prefabPathWithoutPrefabRoot}");
            return null;
        }

        ObjectPool pool = new ObjectPool(prefabPathWithoutPrefabRoot, prefab, parent, defaultCapacity, maxSize);
        poolDict.Add(prefabPathWithoutPrefabRoot, pool);
        return pool;
    }

    public GameObject Spawn(string prefabPathWithoutPrefabRoot, Transform parent = null)
    {
        if (poolDict.TryGetValue(prefabPathWithoutPrefabRoot, out ObjectPool pool) == false)
            pool = CreatePool(prefabPathWithoutPrefabRoot, parent);

        return pool.Spawn();
    }

    public void Despawn(GameObject go)
    {
        if (poolDict.TryGetValue(go.name, out ObjectPool pool))
        {
            pool.Despawn(go);
        }
        else
        {
            Debug.Log($"Failed to despawn : {go.name}");
            Destroy(go);
        }
    }
}