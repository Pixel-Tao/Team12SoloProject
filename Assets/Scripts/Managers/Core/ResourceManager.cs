using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Resources 폴더를 기준으로 파일을 로드하고 인스턴스화하는 매니저
/// 추후 Addressable로 변경할 예정
/// </summary>
public class ResourceManager : IManager
{
    private Dictionary<string, GameObject> prefabDict;
    private Dictionary<string, Sprite> spriteDict;

    public void Init()
    {
        if (prefabDict == null)
            prefabDict = new Dictionary<string, GameObject>();
        if (spriteDict == null)
            spriteDict = new Dictionary<string, Sprite>();
    }
    public void Clear()
    {

    }

    public T Load<T>(string path, bool isMultiple = false) where T : Object
    {
        System.Type t = typeof(T);
        if (t == typeof(GameObject))
        {
            if (prefabDict.TryGetValue(path, out GameObject prefab) == false)
            {
                prefab = Resources.Load<GameObject>(path);
                if (prefab != null)
                    prefabDict.Add(path, prefab);
            }
            return prefab as T;
        }
        else if (t == typeof(Sprite))
        {
            return LoadSprite(path, isMultiple) as T;
        }

        return null;
    }
    private Sprite LoadSprite(string filePath, bool isMultiple = false)
    {
        if (isMultiple)
        {
            // 파일 여러개에서 스프라이트 가져옴
            string multipleSpriteName = filePath.Substring(filePath.LastIndexOf('/') + 1);
            string filePathWithoutName = filePath.Substring(0, filePath.LastIndexOf('/'));
            if (spriteDict.TryGetValue(filePath, out Sprite sprite) == false)
            {
                Sprite[] sprites = Resources.LoadAll<Sprite>(filePathWithoutName);
                if (sprites.Length == 0)
                {
                    Debug.LogError($"Failed to load multiple sprite : {filePathWithoutName}");
                    return null;
                }

                foreach (Sprite s in sprites)
                {
                    if (s.name == multipleSpriteName)
                        sprite = s;

                    spriteDict.Add($"{filePathWithoutName}/{s.name}", s);
                }

                if (sprite == null)
                {
                    Debug.LogError($"Failed to load sprite item : {filePath}");
                    return null;
                }
            }

            return sprite;
        }
        else
        {
            // 파일 하나에서 스프라이트 가져옴
            if (spriteDict.TryGetValue(filePath, out Sprite sprite) == false)
            {
                sprite = Resources.Load<Sprite>(filePath);
                if (sprite == null)
                {
                    Debug.LogWarning($"Failed to load single sprite : {filePath}");
                    return null;
                }
                spriteDict.Add(filePath, sprite);
            }

            return sprite;
        }
    }
    public T[] LoadAll<T>(string path) where T : Object
    {
        T[] items = Resources.LoadAll<T>(path);
        return items;
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
}