using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIShopPopup : UIPopupBase
{
    enum Buttons
    {
        CloseButton,
    }

    enum Texts
    {
        ShopTitleText,
        CarriedGoldText,
    }

    enum GameObjects
    {
        ShopItemList,
        ShopPopup,
    }

    private List<UIShopItemSlot> slots = new List<UIShopItemSlot>();

    protected override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindButton(typeof(Buttons));
        BindText(typeof(Texts));
        BindObject(typeof(GameObjects));

        GetButton(Buttons.CloseButton).gameObject.BindEvent(() => { Close(); });

        return true;
    }

    public override void Open(Defines.UIAnimationType type = Defines.UIAnimationType.None)
    {
        base.Open(type);
        SetDraggable(GetObject(GameObjects.ShopPopup));
        Managers.User.OnGoldChanged += UpdateGold;
    }

    public override void Close(Defines.UIAnimationType type = Defines.UIAnimationType.None)
    {
        ClearDraggable();
        Managers.User.OnGoldChanged -= UpdateGold;
        base.Close(type);
    }

    public void SetData(int shopId)
    {
        // ShopEntity shop = Managers.DB.Get<ShopEntity>(shopId);
        // GetText(Texts.ShopTitleText).text = shop.displayTitle;
        // UpdateGold(Managers.User.CarriedGold);
        //
        // int index = 0;
        // foreach (var sale in shop.shopSaleEntities)
        // {
        //     ItemEntity item = Managers.DB.Get<ItemEntity>(sale.itemId);
        //     AddOrUpdateItemSlot(index, item, sale);
        //     index++;
        // }
        //
        // for (int i = index; i < slots.Count; i++)
        // {
        //     slots[i].gameObject.SetActive(false);
        // }
    }

    private void UpdateGold(long gold)
    {
        GetText(Texts.CarriedGoldText).text = $"{gold:#,##0} G";
    }

    // private void AddOrUpdateItemSlot(int index, ItemEntity item, ShopSaleEntity sale)
    // {
    //     if (index >= slots.Count)
    //     {
    //         GameObject go = Managers.Resource.Instantiate("UI/Slot/UIShopItemSlot", GetObject(GameObjects.ShopItemList).transform);
    //         UIShopItemSlot slot = go.GetOrAddComponent<UIShopItemSlot>();
    //         slot.SetData(item, sale);
    //         slots.Add(slot);
    //     }
    //     else
    //     {
    //         slots[index].SetData(item, sale);
    //         slots[index].gameObject.SetActive(true);
    //     }
    // }
}