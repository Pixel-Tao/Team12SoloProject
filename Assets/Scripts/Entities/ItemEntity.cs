using System;
using System.Collections.Generic;

[Serializable]
public class ItemEntity : EntityBase
{
    public string displayTitle;
    public string description;
    public Defines.ItemType itemType;
    public Defines.ItemRarityType rarityType;
    public string dropPrefabPath;
    public string iconPath;
    
    public ItemEquipableEntity equipableEntity;
    public List<ItemStatBoostEffectEntity> statBoostEffectEntities;
    public List<ItemConsumableEntity> consumableEntities;
}

[Serializable]
public class ItemEquipableEntity
{
    public int itemId;
    public Defines.ItemEquipmentType equipmentType;
    public string equipedPrefabPath;
}

[Serializable]
public class ItemStatBoostEffectEntity
{
    public int itemId;
    public Defines.CharacterStatType statType;
    public int amount;
    public Defines.CalcType calcType;
}

[Serializable]
public class ItemConsumableEntity
{
    public int itemId;
    public Defines.ItemConsumableType consumableType;
    public float amount;
    public float duration;
    public Defines.CalcType calcType;
}