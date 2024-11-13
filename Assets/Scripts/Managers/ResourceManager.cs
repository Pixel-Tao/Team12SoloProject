using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : Singleton<ResourceManager>
{
    private Dictionary<string, GameObject> prefabDict = new Dictionary<string, GameObject>();
    private Dictionary<string, Sprite> spriteDict = new Dictionary<string, Sprite>();

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
                    Debug.LogError($"Failed to load single sprite : {filePath}");
                    return null;
                }
                spriteDict.Add(filePath, sprite);
            }

            return sprite;
        }
    }
    public GameObject Instantiate(string prefabPath, Transform parent = null)
    {
        GameObject prefab = Load<GameObject>($"Prefabs/{prefabPath}");
        if (prefab == null)
        {
            Debug.LogError($"Failed to load prefab : {prefabPath}");
            return null;
        }

        return Instantiate(prefab, parent);
    }
}