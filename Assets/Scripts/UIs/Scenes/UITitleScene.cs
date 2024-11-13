using UnityEngine.SceneManagement;

public class UITitleScene : UISceneBase
{
    public void OnClickStartButton()
    {
        SceneManager.LoadScene("GameScene");
    }
}