using UnityEngine;

/// <summary>
/// Scene 내 동적으로 생성되는 팝업 UI가 상속받아 사용
/// </summary>
public abstract class UIPopupBase : UIBase
{
    public bool IsOpen => gameObject.activeInHierarchy;
    
    public override void Open(Defines.UIAnimationType type = Defines.UIAnimationType.None)
    {
        gameObject.SetActive(true);
        base.Open(type);
    }
    
    public override void Close(Defines.UIAnimationType type = Defines.UIAnimationType.None)
    {
        base.Close(type);
        UIManager.Instance.ClosePopupUI(this);
    }
}
