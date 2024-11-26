using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine.AddressableAssets;
using UnityEngine;

[Serializable]
public class DialogueOptionEntity
{
    /// <summary>
    /// ID
    /// </summary>
    public int key;

    /// <summary>
    /// 이름
    /// </summary>
    public string displayTitle;

}
public class DialogueOptionEntityLoader
{
    public List<DialogueOptionEntity> ItemsList { get; private set; }
    public Dictionary<int, DialogueOptionEntity> ItemsDict { get; private set; }

    public DialogueOptionEntityLoader(Func<string, TextAsset> loadFunc)
    {
        AddDatas(loadFunc("DialogueOptionEntity").text);
    }

    private void AddDatas(string jsonData)
    {
        ItemsList = JsonUtility.FromJson<Wrapper>(jsonData).Items;
        ItemsDict = new Dictionary<int, DialogueOptionEntity>();
        foreach (var item in ItemsList)
        {
            ItemsDict.Add(item.key, item);
        }
    }

    [Serializable]
    private class Wrapper
    {
        public List<DialogueOptionEntity> Items;
    }

    public DialogueOptionEntity GetByKey(int key)
    {
        if (ItemsDict.ContainsKey(key))
        {
            return ItemsDict[key];
        }
        return null;
    }
    public DialogueOptionEntity GetByIndex(int index)
    {
        if (index >= 0 && index < ItemsList.Count)
        {
            return ItemsList[index];
        }
        return null;
    }
}
