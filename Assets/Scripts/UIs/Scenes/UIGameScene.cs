public class UIGameScene : UISceneBase
{
    enum Buttons
    {
        PopupButton,
        TitleButton,
    }
    
    protected override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindButton(typeof(Buttons));

        GetButton(Buttons.PopupButton).gameObject.BindEvent(PopupOpenEvent);
        GetButton(Buttons.TitleButton).gameObject.BindEvent(TitleSceneEvent);

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
}