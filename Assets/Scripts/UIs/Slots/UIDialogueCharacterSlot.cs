using UnityEngine;

public class UIDialoguePortraitSlot : UISlotBase
{
    enum Images
    {
        LeftCharacterImage,
        CenterCharacterImage,
        RightCharacterImage,
    }
    
    
    protected override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindImage(typeof(Images));
        
        return true;
    }
    
    public void Show(Sprite sprite, DesignEnums.PortraitPositionType portraitPositionType)
    {
        switch (portraitPositionType)
        {
            case DesignEnums.PortraitPositionType.Left:
                GetImage(Images.LeftCharacterImage).sprite = sprite;
                GetImage(Images.LeftCharacterImage).gameObject.SetActive(true);
                GetImage(Images.CenterCharacterImage).gameObject.SetActive(false);
                GetImage(Images.RightCharacterImage).gameObject.SetActive(false);
                
                break;
            case DesignEnums.PortraitPositionType.Center:
                GetImage(Images.CenterCharacterImage).sprite = sprite;
                GetImage(Images.CenterCharacterImage).gameObject.SetActive(true);
                GetImage(Images.LeftCharacterImage).gameObject.SetActive(false);
                GetImage(Images.RightCharacterImage).gameObject.SetActive(false);
                break;
            case DesignEnums.PortraitPositionType.Right:
                GetImage(Images.RightCharacterImage).sprite = sprite;
                GetImage(Images.RightCharacterImage).gameObject.SetActive(true);
                GetImage(Images.CenterCharacterImage).gameObject.SetActive(false);
                GetImage(Images.LeftCharacterImage).gameObject.SetActive(false);
                break;
        }
    }

    public void Hide()
    {
        GetImage(Images.LeftCharacterImage).gameObject.SetActive(false);
        GetImage(Images.CenterCharacterImage).gameObject.SetActive(false);
        GetImage(Images.RightCharacterImage).gameObject.SetActive(false);
    }
}