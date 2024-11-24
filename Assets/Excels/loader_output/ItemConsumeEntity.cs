using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine.AddressableAssets;
using UnityEngine;

[Serializable]
public class ItemConsumeEntity
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
    /// 아이템 종류
    /// </summary>
    public DesignEnums.ConsumeType consumeType;

    /// <summary>
    /// 값
    /// </summary>
    public float amount;

    /// <summary>
    /// 지속시간
    /// </summary>
    public float duration;

    /// <summary>
    /// 연산 방식
    /// </summary>
    public DesignEnums.CalcType calcType;

}
public class ItemConsumeEntityLoader
{
    public List<ItemConsumeEntity> ItemsList { get; private set; }
    public Dictionary<int, ItemConsumeEntity> ItemsDict { get; private set; }

    public ItemConsumeEntityLoader(string path = "ItemConsumeEntity")
    {
        Addressables.LoadAssetAsync<TextAsset>(path).Completed += handle => { AddDatas(handle.Result.text); };
    }

    private void AddDatas(string jsonData)
    {
        ItemsList = JsonUtility.FromJson<Wrapper>(jsonData).Items;
        ItemsDict = new Dictionary<int, ItemConsumeEntity>();
        foreach (var item in ItemsList)
        {
            ItemsDict.Add(item.key, item);
        }
    }

    [Serializable]
    private class Wrapper
    {
        public List<ItemConsumeEntity> Items;
    }

    public ItemConsumeEntity GetByKey(int key)
    {
        if (ItemsDict.ContainsKey(key))
        {
            return ItemsDict[key];
        }
        return null;
    }
    public ItemConsumeEntity GetByIndex(int index)
    {
        if (index >= 0 && index < ItemsList.Count)
        {
            return ItemsList[index];
        }
        return null;
    }
}
