using UnityEngine;

public class UIShopItemSlot : UISlotBase
{
    enum Buttons
    {
        BuyButton,
    }

    enum Texts
    {
        ItemTitleText,
        ItemDescriptionText,
        ItemPriceText,
    }

    enum Images
    {
        ItemIconImage,
        ItemRarityImage
    }

    private int id;
    private long price;

    protected override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindButton(typeof(Buttons));
        BindText(typeof(Texts));
        BindImage(typeof(Images));

        GetButton(Buttons.BuyButton).gameObject.BindEvent(() => { Managers.Shop.BuyItem(id, price); });

        return true;
    }

    // public void SetData(ItemEntity item, ShopSaleEntity sale)
    // {
    //     this.id = item.id;
    //     this.price = sale.price;
    //     
    //     GetText(Texts.ItemTitleText).text = item.displayTitle;
    //     GetText(Texts.ItemDescriptionText).text = item.description;
    //     GetText(Texts.ItemPriceText).text = $"{sale.price:#,##0} G";
    //
    //     GetImage(Images.ItemIconImage).sprite = Managers.Resource.Load<Sprite>(item.iconPath);
    //     GetImage(Images.ItemRarityImage).color = Utils.GetItemRarityColor(item.rarityType);
    // }
}