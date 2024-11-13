using UnityEngine;
using UnityObject = UnityEngine.Object;

public static class Extensions
{
    public static bool IsValid(this GameObject obj)
    {
        return obj != null && obj.activeSelf;
    } 
}