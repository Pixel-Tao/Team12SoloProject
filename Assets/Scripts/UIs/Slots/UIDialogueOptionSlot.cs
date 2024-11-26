public class UIDialogueOptionSlot : UISlotBase
{
    enum Texts
    {
        OptionText,
    }
    enum Buttons
    {
        OptionButton,
    }
    
    private UIDialoguePopup popup;
    private DialogueOptionEntity option;
    
    protected override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindButton(typeof(Buttons));
        BindText(typeof(Texts));
        
        GetButton(Texts.OptionText).gameObject.BindEvent(() => { popup?.SelectOption(option); });

        UpdateUI();
        return true;
    }
    
    public void SetData(UIDialoguePopup popup, DialogueOptionEntity option)
    {
        this.popup = popup;
        this.option = option;
        UpdateUI();
    }

    public void UpdateUI()
    {
        var optionText = GetText(Texts.OptionText);
        if (optionText == null) return;
        optionText.text = option?.displayTitle;
    }
    
    public void Clear()
    {
        GetText(Texts.OptionText).text = string.Empty;
        this.option = null;
        this.popup = null;
    }
}