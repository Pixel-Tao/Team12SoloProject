using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEx : IManager
{
    public SceneBase CurrentScene => GameObject.FindObjectOfType<SceneBase>();
    
    public void Init()
    {
        
    }

    public void Clear()
    {
        
    }
    
    public void LoadScene(Defines.SceneType sceneType)
    {
        if (sceneType == Defines.SceneType.None)
        {
            Debug.LogWarning("Invalid SceneType");
            return;
        }

        Managers.Clear();
        SceneManager.LoadScene(sceneType.ToString());
    }
}