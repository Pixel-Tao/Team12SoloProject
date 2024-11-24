using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine.AddressableAssets;
using UnityEngine;

[Serializable]
public class ItemEntity
{
    /// <summary>
    /// ID
    /// </summary>
    public int key;

    /// <summary>
    /// displayTitle
    /// </summary>
    public string displayTitle;

    /// <summary>
    /// description
    /// </summary>
    public string description;

    /// <summary>
    /// 아이템 종류
    /// </summary>
    public DesignEnums.ItemType itemType;

    /// <summary>
    /// 아이템 등급
    /// </summary>
    public DesignEnums.RarityType rarityType;

    /// <summary>
    /// 드랍 아이템 프리팹 Name
    /// </summary>
    public string dropPrefabName;

    /// <summary>
    /// 아이템 아이콘 Name
    /// </summary>
    public string iconName;

}
public class ItemEntityLoader
{
    public List<ItemEntity> ItemsList { get; private set; }
    public Dictionary<int, ItemEntity> ItemsDict { get; private set; }

    public ItemEntityLoader(string path = "ItemEntity")
    {
        Addressables.LoadAssetAsync<TextAsset>(path).Completed += handle => { AddDatas(handle.Result.text); };
    }

    private void AddDatas(string jsonData)
    {
        ItemsList = JsonUtility.FromJson<Wrapper>(jsonData).Items;
        ItemsDict = new Dictionary<int, ItemEntity>();
        foreach (var item in ItemsList)
        {
            ItemsDict.Add(item.key, item);
        }
    }

    [Serializable]
    private class Wrapper
    {
        public List<ItemEntity> Items;
    }

    public ItemEntity GetByKey(int key)
    {
        if (ItemsDict.ContainsKey(key))
        {
            return ItemsDict[key];
        }
        return null;
    }
    public ItemEntity GetByIndex(int index)
    {
        if (index >= 0 && index < ItemsList.Count)
        {
            return ItemsList[index];
        }
        return null;
    }
}
