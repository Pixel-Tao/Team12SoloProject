using System;
using UnityEngine;
using UnityEngine.EventSystems;

public static class Extensions
{
    public static bool IsValid(this GameObject obj)
    {
        return obj != null && obj.activeSelf;
    } 
    
    public static T GetOrAddComponent<T>(this GameObject go) where T : Component
    {
        return Utils.GetOrAddComponent<T>(go);
    }

    public static void BindEvent(this GameObject go, Action action = null, Action<BaseEventData> dragAction = null, Defines.UIEvent type = Defines.UIEvent.Click)
    {
        UIBase.BindEvent(go, action, dragAction, type);
    }
}