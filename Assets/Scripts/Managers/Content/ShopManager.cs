using UnityEngine;

public class ShopManager : IManager
{
    public void Init()
    {
        
    }

    public void Clear()
    {

    }

    public void BuyItem(int id, long price)
    {
        ItemEntity item = Managers.DB.Get<ItemEntity>(id);
        Managers.User.Payment(price);
        Debug.Log($"{item.displayTitle} 아이템을 {price}G에 구매하였습니다.");
    }
}