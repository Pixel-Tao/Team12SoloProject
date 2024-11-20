using UnityEngine;

public class Utils
{
    public static T GetOrAddComponent<T>(GameObject go) where T : Component
    {
        if (go.TryGetComponent(out T component) == false)
            component = go.AddComponent<T>();
        return component;
    }

    public static GameObject FindChild(GameObject go, string name = null, bool recursive = false)
    {
        Transform transform = FindChild<Transform>(go, name, recursive);
        if (transform == null)
            return null;

        return transform.gameObject;
    }

    public static T FindChild<T>(GameObject go, string name = null, bool recursive = false) where T : UnityEngine.Object
    {
        if (go == null)
            return null;

        if (recursive == false)
        {
            for (int i = 0; i < go.transform.childCount; i++)
            {
                Transform transform = go.transform.GetChild(i);
                if (string.IsNullOrEmpty(name) || transform.name == name)
                {
                    T component = transform.GetComponent<T>();
                    if (component != null)
                        return component;
                }
            }
        }
        else
        {
            foreach (T component in go.GetComponentsInChildren<T>())
            {
                if (string.IsNullOrEmpty(name) || component.name == name)
                    return component;
            }
        }

        return null;
    }

    public static Color HexToColor(string hex)
    {
        // hex = "#FF00FFFF"
        if (!hex.StartsWith("#"))
            hex = $"#{hex}";

        if (ColorUtility.TryParseHtmlString(hex, out Color color))
            return color;

        return Color.clear;
    }

    public static Color GetItemRarityColor(Defines.ItemRarityType type)
    {
        return type switch
        {
            Defines.ItemRarityType.Common => HexToColor(Defines.ITEM_RARIY_COMMON_COLOR),
            Defines.ItemRarityType.Rare => HexToColor(Defines.ITEM_RARIY_RARE_COLOR),
            Defines.ItemRarityType.Epic => HexToColor(Defines.ITEM_RARIY_EPIC_COLOR),
            Defines.ItemRarityType.Unique => HexToColor(Defines.ITEM_RARIY_UNIQUE_COLOR),
            _ => Color.white,
        };
    }
    
    public static string GetFileSize(long byteCnt)
    {
        string size = "0 Bytes";
        if (byteCnt >= 1073741824.0) // 1GB
        {
            size = string.Format("{0:##.##}", byteCnt / 1073741824.0) + " GB";
        }
        else if (byteCnt >= 1048576.0) // 1MB
        {
            size = string.Format("{0:##.##}", byteCnt / 1048576.0) + " MB";
        }
        else if (byteCnt >= 1024.0) // 1KB
        {
            size = string.Format("{0:##.##}", byteCnt / 1024.0) + " KB";
        }
        else if (byteCnt > 0 && byteCnt < 1024.0)
        {
            size = byteCnt.ToString() + " Bytes";
        }
    
        return size;
    }
}