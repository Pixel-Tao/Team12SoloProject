using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine.AddressableAssets;
using UnityEngine;

[Serializable]
public class ShopSaleEntity
{
    /// <summary>
    /// ID
    /// </summary>
    public int key;

    /// <summary>
    /// shopId
    /// </summary>
    public int shopId;

    /// <summary>
    /// itemId
    /// </summary>
    public int itemId;

    /// <summary>
    /// 아이템 가격
    /// </summary>
    public long price;

}
public class ShopSaleEntityLoader
{
    public List<ShopSaleEntity> ItemsList { get; private set; }
    public Dictionary<int, ShopSaleEntity> ItemsDict { get; private set; }

    public ShopSaleEntityLoader(Func<string, TextAsset> loadFunc)
    {
        AddDatas(loadFunc("ShopSaleEntity").text);
    }

    private void AddDatas(string jsonData)
    {
        ItemsList = JsonUtility.FromJson<Wrapper>(jsonData).Items;
        ItemsDict = new Dictionary<int, ShopSaleEntity>();
        foreach (var item in ItemsList)
        {
            ItemsDict.Add(item.key, item);
        }
    }

    [Serializable]
    private class Wrapper
    {
        public List<ShopSaleEntity> Items;
    }

    public ShopSaleEntity GetByKey(int key)
    {
        if (ItemsDict.ContainsKey(key))
        {
            return ItemsDict[key];
        }
        return null;
    }
    public ShopSaleEntity GetByIndex(int index)
    {
        if (index >= 0 && index < ItemsList.Count)
        {
            return ItemsList[index];
        }
        return null;
    }
}
