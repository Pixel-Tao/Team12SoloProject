using UnityEngine.UI;

public class UIMyPopup : UIPopupBase
{
    enum Buttons
    {
        CloseButton,
    }

    protected override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindButton(typeof(Buttons));

        GetButton(Buttons.CloseButton).gameObject.BindEvent(() => { Close(); });

        return true;
    }
    
}