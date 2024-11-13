using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDataList", menuName = "SO/ItemDataList", order = 0)]
public class MonsterDataList : ScriptableObject
{
    public List<MonsterEntity> MonsterList;
    
    public static ScriptableObject Convert(Sheet sheet)
    {
        MonsterDataList monsterDataList = ScriptableObject.CreateInstance<MonsterDataList>();

        if (sheet is MonsterSheets monsterSheets)
        {
            monsterDataList.MonsterList = monsterSheets.MonsterList;
            foreach (var monster in monsterDataList.MonsterList)
            {
                monster.MonsterDropItemEntities = new List<MonsterDropItemEntity>();
                foreach (var monsterDropItem in monsterSheets.MonsterDropItemList)
                    if (monsterDropItem.monsterId == monster.id)
                        monster.MonsterDropItemEntities.Add(monsterDropItem);
            }
        }

        return monsterDataList;
    }
}