using System.Collections.Generic;
using UnityEngine;

public class DBManager : IManager
{
    private const string dataListDirPath = "DataList";

    private Dictionary<int, EntityBase> itemDb = new Dictionary<int, EntityBase>();
    private Dictionary<int, EntityBase> monsterDb = new Dictionary<int, EntityBase>();
    private Dictionary<int, EntityBase> shopDb = new Dictionary<int, EntityBase>();

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
        ItemDataList itemDataList = LoadDataList<ItemDataList>();
        foreach (ItemEntity itemEntity in itemDataList.ItemList)
            itemDb.Add(itemEntity.id, itemEntity);
        
        Debug.Log($"Item Loaded Count : {itemDb.Count}");
    }
    private void LoadMonsterDb()
    {
        MonsterDataList monsterDataList = LoadDataList<MonsterDataList>();
        foreach (MonsterEntity monsterEntity in monsterDataList.MonsterList)
            monsterDb.Add(monsterEntity.id, monsterEntity);
        
        Debug.Log($"Monster Loaded Count : {monsterDb.Count}");
    }
    
    private void LoadShopDb()
    {
        ShopDataList shopDataList = LoadDataList<ShopDataList>();
        foreach (ShopEntity shopEntity in shopDataList.ShopList)
            shopDb.Add(shopEntity.id, shopEntity);
        
        Debug.Log($"Shop Loaded Count : {shopDb.Count}");
    }

    public T Get<T>(int id) where T : EntityBase
    {
        if (typeof(T) == typeof(ItemEntity))
        {
            if (itemDb.TryGetValue(id, out EntityBase value))
            {
                return value as T;
            }
        }
        else if (typeof(T) == typeof(MonsterEntity))
        {
            if (monsterDb.TryGetValue(id, out EntityBase value))
            {
                return value as T;
            }
        }
        else if (typeof(T) == typeof(ShopEntity))
        {
            if (shopDb.TryGetValue(id, out EntityBase value))
            {
                return value as T;
            }
        }

        return null;
    }
    
    public int Count<T>() where T : EntityBase
    {
        if (typeof(T) == typeof(ItemEntity))
            return itemDb.Count;
        else if (typeof(T) == typeof(MonsterEntity))
            return monsterDb.Count;

        return 0;
    }
}