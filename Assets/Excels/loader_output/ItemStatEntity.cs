using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

[Serializable]
public class ItemStatEntity
{
    /// <summary>
    /// ID
    /// </summary>
    public int key;

    /// <summary>
    /// 아이템 ID
    /// </summary>
    public int itemId;

    /// <summary>
    /// 적용 스텟 타입
    /// </summary>
    public DesignEnums.StatType statType;

    /// <summary>
    /// 값
    /// </summary>
    public float amount;

    /// <summary>
    /// 연산 방식
    /// </summary>
    public DesignEnums.CalcType calcType;

}
public class ItemStatEntityLoader
{
    public List<ItemStatEntity> ItemsList { get; private set; }
    public Dictionary<int, ItemStatEntity> ItemsDict { get; private set; }

    public ItemStatEntityLoader(string path)
    {
        string jsonData;
        jsonData = File.ReadAllText(path);
        ItemsList = JsonUtility.FromJson<Wrapper>(jsonData).Items;
        ItemsDict = new Dictionary<int, ItemStatEntity>();
        foreach (var item in ItemsList)
        {
            ItemsDict.Add(item.key, item);
        }
    }

    [Serializable]
    private class Wrapper
    {
        public List<ItemStatEntity> Items;
    }

    public ItemStatEntity GetByKey(int key)
    {
        if (ItemsDict.ContainsKey(key))
        {
            return ItemsDict[key];
        }
        return null;
    }
    public ItemStatEntity GetByIndex(int index)
    {
        if (index >= 0 && index < ItemsList.Count)
        {
            return ItemsList[index];
        }
        return null;
    }
}
