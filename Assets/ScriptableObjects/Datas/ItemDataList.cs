using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDataList", menuName = "SO/ItemDataList", order = 0)]
public class ItemDataList : ScriptableObject
{
    public List<ItemEntity> ItemList;

    public static ScriptableObject Convert(Sheet sheet)
    {
        ItemDataList itemDataList = ScriptableObject.CreateInstance<ItemDataList>();

        if (sheet is ItemSheets itemSheets)
        {
            itemDataList.ItemList = itemSheets.ItemList;
            foreach (var item in itemDataList.ItemList)
            {
                foreach (var equipable in itemSheets.EquipableList)
                    if (equipable.itemId == item.id && item.itemType == Defines.ItemType.Equipment)
                        item.equipableEntity = equipable;

                item.statBoostEffectEntities = new List<ItemStatBoostEffectEntity>();
                foreach (var statBoostEffect in itemSheets.StatBoostEffectList)
                    if (statBoostEffect.itemId == item.id && item.itemType == Defines.ItemType.Equipment)
                        item.statBoostEffectEntities.Add(statBoostEffect);

                item.consumableEntities = new List<ItemConsumableEntity>();
                foreach (var consumable in itemSheets.ConsumableList)
                    if (consumable.itemId == item.id && item.itemType == Defines.ItemType.Consumable)
                        item.consumableEntities.Add(consumable);
            }
        }

        return itemDataList;
    }
}