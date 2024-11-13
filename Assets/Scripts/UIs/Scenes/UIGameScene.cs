public class UIGameScene : UISceneBase
{
    public void PopupOpen()
    {
        UIManager.Instance.ShowPopupUI<UIMyPopup>();
    }
}