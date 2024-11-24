using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine.AddressableAssets;
using UnityEngine;

[Serializable]
public class DialogueLineEntity
{
    /// <summary>
    /// ID
    /// </summary>
    public int key;

    /// <summary>
    /// 대화 주제
    /// </summary>
    public int dialogueId;

    /// <summary>
    /// 대화 순서
    /// </summary>
    public int seq;

    /// <summary>
    /// 이름 (다른 익명으로 보여주려면 값 입력)
    /// </summary>
    public string displaySubTitle;

    /// <summary>
    /// 대화 내용
    /// </summary>
    public string text;

    /// <summary>
    /// 대화 선택지
    /// </summary>
    public List<int> options;

}
public class DialogueLineEntityLoader
{
    public List<DialogueLineEntity> ItemsList { get; private set; }
    public Dictionary<int, DialogueLineEntity> ItemsDict { get; private set; }

    public DialogueLineEntityLoader(string path = "DialogueLineEntity")
    {
        Addressables.LoadAssetAsync<TextAsset>(path).Completed += handle => { AddDatas(handle.Result.text); };
    }

    private void AddDatas(string jsonData)
    {
        ItemsList = JsonUtility.FromJson<Wrapper>(jsonData).Items;
        ItemsDict = new Dictionary<int, DialogueLineEntity>();
        foreach (var item in ItemsList)
        {
            ItemsDict.Add(item.key, item);
        }
    }

    [Serializable]
    private class Wrapper
    {
        public List<DialogueLineEntity> Items;
    }

    public DialogueLineEntity GetByKey(int key)
    {
        if (ItemsDict.ContainsKey(key))
        {
            return ItemsDict[key];
        }
        return null;
    }
    public DialogueLineEntity GetByIndex(int index)
    {
        if (index >= 0 && index < ItemsList.Count)
        {
            return ItemsList[index];
        }
        return null;
    }
}
