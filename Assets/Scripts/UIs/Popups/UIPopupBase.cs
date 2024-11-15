using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Scene 내 동적으로 생성되는 팝업 UI가 상속받아 사용
/// </summary>
public abstract class UIPopupBase : UIBase
{
    public bool IsOpen => gameObject.activeInHierarchy;
    private GameObject popupGo;
    
    public override void Open(Defines.UIAnimationType type = Defines.UIAnimationType.None)
    {
        gameObject.SetActive(true);
        base.Open(type);
    }
    
    public override void Close(Defines.UIAnimationType type = Defines.UIAnimationType.None)
    {
        base.Close(type);
        Managers.UI.ClosePopupUI(this);
    }

    protected void SetDraggable(GameObject targetGo)
    {
        popupGo = targetGo;
        popupGo.BindEvent(dragAction: BeginDragEvent, type: Defines.UIEvent.BeginDrag);
        popupGo.BindEvent(dragAction: DraggingEvent, type: Defines.UIEvent.Drag);
        popupGo.BindEvent(dragAction: EndDragEvent, type: Defines.UIEvent.EndDrag);
    }
    
    protected void ClearDraggable()
    {
        if (popupGo == null) return;
        popupGo.UnBindEvent(dragAction: BeginDragEvent, type: Defines.UIEvent.BeginDrag);
        popupGo.UnBindEvent(dragAction: DraggingEvent, type: Defines.UIEvent.Drag);
        popupGo.UnBindEvent(dragAction: EndDragEvent, type: Defines.UIEvent.EndDrag);
        popupGo = null;
    }
    
    protected virtual void DraggingEvent(BaseEventData data)
    {
        PointerEventData ped = data as PointerEventData;
        if (ped == null)
            return;
        
        Debug.Log($"DraggingEvent : {ped}");
        popupGo.transform.position += (Vector3)ped.delta;
    }
    
    protected virtual void BeginDragEvent(BaseEventData data)
    {
        Debug.Log("BeginDragEvent");
        popupGo.transform.SetAsLastSibling();
    }
    
    protected virtual void EndDragEvent(BaseEventData data)
    {
        Debug.Log("EndDragEvent");
        popupGo.transform.SetAsFirstSibling();
    }
}
