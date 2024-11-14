using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDataList", menuName = "SO/ItemDataList", order = 0)]
public class ShopDataList : ScriptableObject
{
    public List<ShopEntity> ShopList;
    
    public static ScriptableObject Convert(Sheet sheet)
    {
        ShopDataList shopDataList = ScriptableObject.CreateInstance<ShopDataList>();

        if (sheet is ShopSheets shopSheets)
        {
            shopDataList.ShopList = shopSheets.ShopList;
            foreach (var monster in shopDataList.ShopList)
            {
                monster.shopSaleEntities = new List<ShopSaleEntity>();
                foreach (var monsterDropItem in shopSheets.ShopSaleList)
                    if (monsterDropItem.shopId == monster.id)
                        monster.shopSaleEntities.Add(monsterDropItem);
            }
        }

        return shopDataList;
    }
}