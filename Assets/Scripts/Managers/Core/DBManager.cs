using System.Collections.Generic;
using UnityEngine;

public class DBManager : IManager
{
    private const string dataListDirPath = "DataList";
    

    public void Init()
    {
        LoadItemDb();
        LoadMonsterDb();
        LoadShopDb();
    }
    public void Clear()
    {
        
    }

    private T LoadDataList<T>() where T : ScriptableObject
    {
        T dataList = Managers.Resource.Load<T>($"{dataListDirPath}/{typeof(T).Name}");
        if (dataList == null)
            Debug.LogError($"Failed to load {nameof(dataListDirPath)}");
        
        return dataList;
    }

    private void LoadItemDb()
    {
        // ItemDataList itemDataList = LoadDataList<ItemDataList>();
        // foreach (ItemEntity itemEntity in itemDataList.ItemList)
        //     itemDb.Add(itemEntity.id, itemEntity);
        //
        // Debug.Log($"Item Loaded Count : {itemDb.Count}");
    }
    private void LoadMonsterDb()
    {
        // MonsterDataList monsterDataList = LoadDataList<MonsterDataList>();
        // foreach (MonsterEntity monsterEntity in monsterDataList.MonsterList)
        //     monsterDb.Add(monsterEntity.id, monsterEntity);
        //
        // Debug.Log($"Monster Loaded Count : {monsterDb.Count}");
    }
    
    private void LoadShopDb()
    {
        // ShopDataList shopDataList = LoadDataList<ShopDataList>();
        // foreach (ShopEntity shopEntity in shopDataList.ShopList)
        //     shopDb.Add(shopEntity.id, shopEntity);
        //
        // Debug.Log($"Shop Loaded Count : {shopDb.Count}");
    }
}