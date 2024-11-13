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

    private ObjectPool CreatePool(GameObject prefab, Transform parent = null)
    {
        ObjectPool pool = new ObjectPool(prefab, parent, defaultCapacity, maxSize);
        poolDict.Add(prefab.name, pool);
        return pool;
    }
    
    public GameObject Spawn(GameObject prefab, Transform parent = null)
    {
        if (poolDict.TryGetValue(prefab.name, out ObjectPool pool) == false)
            pool = CreatePool(prefab, parent);
        
        return pool.Pop();
    }

    public GameObject Spawn(string prefabPathWithoutPrefabRoot, Transform parent = null)
    {
        string name = prefabPathWithoutPrefabRoot.Substring(prefabPathWithoutPrefabRoot.LastIndexOf('/') + 1);

        if (poolDict.TryGetValue(name, out ObjectPool pool) == false)
        {
            GameObject prefab = ResourceManager.Instance.Load<GameObject>($"Prefabs/{prefabPathWithoutPrefabRoot}");
            if (prefab == null)
            {
                Debug.Log($"Failed to load prefab : {prefabPathWithoutPrefabRoot}");
                return null;
            }
            
            pool = CreatePool(prefab, parent);
        }
        
        return pool.Pop();
    }
    
    public void Despawn(GameObject go)
    {
        if (poolDict.TryGetValue(go.name, out ObjectPool pool))
        {
            pool.Push(go);
        }
        else
        {
            Debug.Log($"Failed to despawn : {go.name}");
            Destroy(go);
        }
    }
}