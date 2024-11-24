using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine.AddressableAssets;
using UnityEngine;

[Serializable]
public class NpcEntity
{
    /// <summary>
    /// ID
    /// </summary>
    public int key;

    /// <summary>
    /// 이름
    /// </summary>
    public string displayTitle;

    /// <summary>
    /// 설명
    /// </summary>
    public string description;

    /// <summary>
    /// 이미지 스프라이트 name
    /// </summary>
    public string imageName;

    /// <summary>
    /// 프리팹 이름
    /// </summary>
    public string prefabName;

    /// <summary>
    /// NPC 대화 선택지
    /// </summary>
    public List<int> dialogueIds;

}
public class NpcEntityLoader
{
    public List<NpcEntity> ItemsList { get; private set; }
    public Dictionary<int, NpcEntity> ItemsDict { get; private set; }

    public NpcEntityLoader(string path = "NpcEntity")
    {
        Addressables.LoadAssetAsync<TextAsset>(path).Completed += handle => { AddDatas(handle.Result.text); };
    }

    private void AddDatas(string jsonData)
    {
        ItemsList = JsonUtility.FromJson<Wrapper>(jsonData).Items;
        ItemsDict = new Dictionary<int, NpcEntity>();
        foreach (var item in ItemsList)
        {
            ItemsDict.Add(item.key, item);
        }
    }

    [Serializable]
    private class Wrapper
    {
        public List<NpcEntity> Items;
    }

    public NpcEntity GetByKey(int key)
    {
        if (ItemsDict.ContainsKey(key))
        {
            return ItemsDict[key];
        }
        return null;
    }
    public NpcEntity GetByIndex(int index)
    {
        if (index >= 0 && index < ItemsList.Count)
        {
            return ItemsList[index];
        }
        return null;
    }
}
