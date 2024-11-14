public class UIGameScene : UISceneBase
{
    enum Buttons
    {
        PopupButton,
        TitleButton,
        PotionShopButton,
        WeaponShopButton,
        ArmorShopButton,
        AccessoryShopButton,
    }
    
    protected override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindButton(typeof(Buttons));

        GetButton(Buttons.PopupButton).gameObject.BindEvent(PopupOpenEvent);
        GetButton(Buttons.TitleButton).gameObject.BindEvent(TitleSceneEvent);
        GetButton(Buttons.PotionShopButton).gameObject.BindEvent(PotionShopEvent);
        GetButton(Buttons.WeaponShopButton).gameObject.BindEvent(WeaponShopEvent);
        GetButton(Buttons.ArmorShopButton).gameObject.BindEvent(ArmorShopEvent);
        GetButton(Buttons.AccessoryShopButton).gameObject.BindEvent(AccessoryShopEvent);

        return true;
    }
    
    public void PopupOpenEvent()
    {
        Managers.UI.ShowPopupUI<UIMyPopup>();
    }
    
    public void TitleSceneEvent()
    {
        Managers.Scene.LoadScene(Defines.SceneType.TitleScene);
    }
    
    public void PotionShopEvent()
    {
        Managers.UI.ShowPopupUI<UIShopPopup>().SetData(501);
    }
    
    public void WeaponShopEvent()
    {
        Managers.UI.ShowPopupUI<UIShopPopup>().SetData(502);
    }
    
    public void ArmorShopEvent()
    {
        Managers.UI.ShowPopupUI<UIShopPopup>().SetData(503);
    }
    
    public void AccessoryShopEvent()
    {
        Managers.UI.ShowPopupUI<UIShopPopup>().SetData(504);
    }
}