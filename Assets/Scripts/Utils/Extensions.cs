using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public static class Extensions
{
    public static bool IsValid(this GameObject obj)
    {
        return obj != null && obj.activeSelf;
    }
    
    public static bool IsValid(this Transform obj)
    {
        return obj != null && obj.gameObject.IsValid();
    }
    
    public static T GetOrAddComponent<T>(this GameObject go) where T : Component
    {
        return Utils.GetOrAddComponent<T>(go);
    }

    public static void BindEvent(this GameObject go, Action action = null, Action<BaseEventData> dragAction = null, Defines.UIEvent type = Defines.UIEvent.Click)
    {
        UIBase.BindEvent(go, action, dragAction, type);
    }

    public static void UnBindEvent(this GameObject go, Action action = null, Action<BaseEventData> dragAction = null, Defines.UIEvent type = Defines.UIEvent.Click)
    {
        UIBase.UnBindEvent(go);
    }
    
    public static void SafeScreenMatch(this CanvasScaler scaler, float ratio = 0.5625f)
    {
        if (scaler != null)
        {
            float safeRatio = Screen.safeArea.width / Screen.safeArea.height;
            if (safeRatio > ratio)
                scaler.matchWidthOrHeight = 1f;
        }
    }
}