using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine.AddressableAssets;
using UnityEngine;

[Serializable]
public class DialogueEntity
{
    /// <summary>
    /// ID
    /// </summary>
    public int key;

    /// <summary>
    /// 대화 목적
    /// </summary>
    public DesignEnums.NpcInteractionType npcInteractionType;

    /// <summary>
    /// 이름
    /// </summary>
    public string displayTitle;

    /// <summary>
    /// 설명
    /// </summary>
    public string description;

}
public class DialogueEntityLoader
{
    public List<DialogueEntity> ItemsList { get; private set; }
    public Dictionary<int, DialogueEntity> ItemsDict { get; private set; }

    public DialogueEntityLoader(string path = "DialogueEntity")
    {
        Addressables.LoadAssetAsync<TextAsset>(path).Completed += handle => { AddDatas(handle.Result.text); };
    }

    private void AddDatas(string jsonData)
    {
        ItemsList = JsonUtility.FromJson<Wrapper>(jsonData).Items;
        ItemsDict = new Dictionary<int, DialogueEntity>();
        foreach (var item in ItemsList)
        {
            ItemsDict.Add(item.key, item);
        }
    }

    [Serializable]
    private class Wrapper
    {
        public List<DialogueEntity> Items;
    }

    public DialogueEntity GetByKey(int key)
    {
        if (ItemsDict.ContainsKey(key))
        {
            return ItemsDict[key];
        }
        return null;
    }
    public DialogueEntity GetByIndex(int index)
    {
        if (index >= 0 && index < ItemsList.Count)
        {
            return ItemsList[index];
        }
        return null;
    }
}
