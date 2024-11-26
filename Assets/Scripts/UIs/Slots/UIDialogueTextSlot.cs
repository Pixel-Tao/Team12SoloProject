using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIDialogueTextSlot : UISlotBase
{
    enum Texts
    {
        DisplaySubTitle,
        LineText,
    }
    
    enum Images
    {
        NextIcon,
    }
    
    [SerializeField] private float textSpeed = 0.1f;
    private DialogueLineEntity line;
    private WaitForSeconds wait;
    private UIDialoguePopup popup;
    
    private int currentTextPosition;
    private TMP_Text lineText;
    private bool isSkip;
    public bool IsPlaying { get; private set; }
    
    protected override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindText(typeof(Texts));
        BindImage(typeof(Images));
        wait = new WaitForSeconds(textSpeed);
        lineText = GetText(Texts.LineText);
        return true;
    }
    
    public void SetData(UIDialoguePopup popup, DialogueLineEntity line, string name)
    {
        this.popup = popup;
        this.line = line;
        IsPlaying = false;
        currentTextPosition = 0;
        GetText(Texts.DisplaySubTitle).text = string.IsNullOrWhiteSpace(line.displaySubTitle) ? name : line.displaySubTitle;
    }

    public void Play()
    {
        // TODO: 텍스트 출력 애니메이션
        IsPlaying = true;
        isSkip = false;
        GetImage(Images.NextIcon).gameObject.SetActive(false);
        StartCoroutine(PlayTextAnimation());
    }
    
    private IEnumerator PlayTextAnimation()
    {
        while (currentTextPosition < line.text.Length && isSkip == false)
        {
            yield return wait;
            currentTextPosition++;
            lineText.text = line.text.Substring(0, currentTextPosition);
        }

        Stop();
    }

    public void Skip()
    {
        if (isSkip) return;
        isSkip = true;
        Stop();
    }

    public void Stop()
    {
        lineText.text = line.text;
        IsPlaying = false;
        GetImage(Images.NextIcon).gameObject.SetActive(true);
    }
    
    public void Clear()
    {
        GetText(Texts.DisplaySubTitle).text = string.Empty;
        GetText(Texts.LineText).text = string.Empty;
        GetImage(Images.NextIcon).gameObject.SetActive(false);
    }
}