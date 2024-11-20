using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceLocations;
using UObject = UnityEngine.Object;

/// <summary>
/// Resources 폴더를 기준으로 파일을 로드하고 인스턴스화하는 매니저
/// 추후 Addressable로 변경할 예정
/// </summary>
public class ResourceManager : IManager
{
    private Dictionary<string, UObject> resourceDict;

    public void Init()
    {
        if (resourceDict == null)
            resourceDict = new Dictionary<string, UObject>();
    }
    public void Clear()
    {

    }
    public T Load<T>(string name) where T : UObject
    {
        if (resourceDict.TryGetValue(name, out UObject resource))
        {
            return resource as T;
        }

        return null;
    }


    public GameObject Instantiate(string prefabPath, Transform parent = null)
    {
        GameObject prefab = Load<GameObject>($"Prefabs/{prefabPath}");
        if (prefab == null)
        {
            Debug.LogError($"Failed to load prefab : {prefabPath}");
            return null;
        }

        return GameObject.Instantiate(prefab, parent);
    }

    public void Destroy(GameObject go)
    {
        GameObject.Destroy(go);
    }


    #region Addressable

    public void LoadAsync<T>(string key, Action<T> callback = null) where T : UnityEngine.Object
    {
        if(resourceDict.TryGetValue(key, out UnityEngine.Object resource))
        {
            return;
        }

        string loadKey = key;
        if(key.Contains(".sprite"))
            loadKey = $"{key}[{key.Replace(".sprite", "")}]";

        var asyncOperation = Addressables.LoadAssetAsync<T>(loadKey);
        asyncOperation.Completed += (op) => 
        {
            if(resourceDict.ContainsKey(key) == false)
            {
                resourceDict.Add(key, op.Result);
                callback?.Invoke(op.Result);
            }
            else
            {
                Debug.LogWarning($"Already loaded resource: {key}");
                callback?.Invoke(op.Result);
            }
        }; 
    }

    public void LoadAllAsync<T>(string label, Action<string, int, int> callback) where T : UnityEngine.Object
    {
        var opHandle = Addressables.LoadResourceLocationsAsync(label, typeof(T));
        opHandle.Completed += (op) =>
        {
            var locations = op.Result;
            int totalCount = locations.Count;
            int currentCount = 0;

            foreach (var location in locations)
            {
                var key = location.PrimaryKey;
                if (location.PrimaryKey.Contains(".sprite") || location.InternalId.Contains(".png"))
                {
                    LoadAsync<Sprite>(location.PrimaryKey, (obj) =>
                    {
                        currentCount++;
                        callback?.Invoke(location.PrimaryKey, currentCount, totalCount);
                    });
                }
                else
                {
                    LoadAsync<T>(key, (obj) =>
                    {
                        currentCount++;
                        callback?.Invoke(key, currentCount, totalCount);
                    });
                }
            }
        };
    }
    public async void LoadAllAsync<T>(string[] labels, Action<string, string, int, int> callback) where T : UnityEngine.Object
    {
        int totalCount = 0;
        int currentCount = 0;
        Dictionary<string, IList<IResourceLocation>> locationDict = new Dictionary<string, IList<IResourceLocation>>();
        foreach (var label in labels)
        {
            var opHandle = Addressables.LoadResourceLocationsAsync(label, typeof(T));
            var locations = await opHandle.Task;
            locationDict.Add(label, locations);
            totalCount += locations.Count;
        }
        
        foreach (var label in labels)
        {
            var locations = locationDict[label];
            foreach (var location in locations)
            {
                var key = location.PrimaryKey;
                if (location.PrimaryKey.Contains(".sprite") || location.InternalId.Contains(".png"))
                {
                    LoadAsync<Sprite>(location.PrimaryKey, (obj) =>
                    {
                        currentCount++;
                        callback?.Invoke(label, location.PrimaryKey, currentCount, totalCount);
                    });
                }
                else
                {
                    LoadAsync<T>(key, (obj) =>
                    {
                        currentCount++;
                        callback?.Invoke(label, key, currentCount, totalCount);
                    });
                }
            }
        }
    }
    #endregion
}