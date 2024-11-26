using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine.AddressableAssets;
using UnityEngine;

[Serializable]
public class ItemEquipEntity
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
    /// 장비 타입
    /// </summary>
    public DesignEnums.EquipmentType equipmentType;

    /// <summary>
    /// 장착 프리팹 Name
    /// </summary>
    public string equipedPrefabName;

}
public class ItemEquipEntityLoader
{
    public List<ItemEquipEntity> ItemsList { get; private set; }
    public Dictionary<int, ItemEquipEntity> ItemsDict { get; private set; }

    public ItemEquipEntityLoader(Func<string, TextAsset> loadFunc)
    {
        AddDatas(loadFunc("ItemEquipEntity").text);
    }

    private void AddDatas(string jsonData)
    {
        ItemsList = JsonUtility.FromJson<Wrapper>(jsonData).Items;
        ItemsDict = new Dictionary<int, ItemEquipEntity>();
        foreach (var item in ItemsList)
        {
            ItemsDict.Add(item.key, item);
        }
    }

    [Serializable]
    private class Wrapper
    {
        public List<ItemEquipEntity> Items;
    }

    public ItemEquipEntity GetByKey(int key)
    {
        if (ItemsDict.ContainsKey(key))
        {
            return ItemsDict[key];
        }
        return null;
    }
    public ItemEquipEntity GetByIndex(int index)
    {
        if (index >= 0 && index < ItemsList.Count)
        {
            return ItemsList[index];
        }
        return null;
    }
}
