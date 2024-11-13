using UnityEngine.SceneManagement;

public class UITitleScene : UISceneBase
{
    enum Buttons
    {
        StartButton,
    }
    
    protected override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindButton(typeof(Buttons));

        GetButton(Buttons.StartButton).gameObject.BindEvent(StartButtonEvent);

        return true;
    }
    
    
    public void StartButtonEvent()
    {
        Managers.Scene.LoadScene(Defines.SceneType.GameScene);
    }
}