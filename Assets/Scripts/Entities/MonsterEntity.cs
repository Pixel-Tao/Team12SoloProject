using System;
using System.Collections.Generic;

[Serializable]
public class MonsterEntity : EntityBase
{
    public string displayTitle;
    public string description;
    public int level;
    public string prefabPath;
    public string iconPath;

    public List<MonsterDropItemEntity> MonsterDropItemEntities;
}

[Serializable]
public class MonsterDropItemEntity
{
    public int monsterId;
    public int itemId;
    public int dropRate;
}