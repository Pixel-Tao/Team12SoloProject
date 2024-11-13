using UnityEngine;

/// <summary>
/// Scene 내 동적으로 생성되는 팝업 UI가 상속받아 사용
/// </summary>
public abstract class UIPopupBase : UIBase
{
    public bool IsOpen => gameObject.activeInHierarchy;
    
    public override void Open()
    {
        gameObject.SetActive(true);
    }
    
    public override void Close()
    {
        UIManager.Instance.ClosePopupUI(this);
    }
}
