using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIDialoguePopup : UIPopupBase
{
    enum Objects
    {
        UIDialogueCharacterSlot, // 대화 캐릭터 이미지 노출
        OptionContainer,
    }

    enum Buttons
    {
        UIDialogueTextSlot,
    }

    private List<UIDialogueOptionSlot> optionSlots;
    private DialogueEntity dialogue;
    private List<DialogueLineEntity> lines;
    private UIDialogueTextSlot textSlot;
    private UIDialogueOptionSlot optionSlot;
    private UIDialoguePortraitSlot characterSlot;

    private int currentLineIndex;
    private int sequence;
    private string characterName;

    private Transform optionSlotParent;
    private GameObject optionContainer;
    private DialogueOptionEntity selectedOption;
    protected override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindObject(typeof(Objects));
        BindButton(typeof(Buttons));

        optionContainer = Get<GameObject>((int)Objects.OptionContainer);
        optionSlotParent = optionContainer.transform.GetChild(0);
        characterSlot = Get<GameObject>((int)Objects.UIDialogueCharacterSlot).GetComponent<UIDialoguePortraitSlot>();
        optionContainer.SetActive(false);
        optionSlots = new List<UIDialogueOptionSlot>();
        textSlot = GetButton(Buttons.UIDialogueTextSlot)?.GetComponent<UIDialogueTextSlot>();
        GetButton(Buttons.UIDialogueTextSlot).gameObject.BindEvent(DialogueClick);
        return true;
    }

    public void SetData(int dialogueId, string characterName)
    {
        this.characterName = characterName;
        dialogue = Managers.DB.DialogueEntityLoader.GetByKey(dialogueId);

        if (dialogue == null)
        {
            Debug.LogWarning("Dialogue Not Found");
            return;
        }
        optionContainer.SetActive(false);
        currentLineIndex = -1;
        lines = Managers.DB.DialogueLineEntityLoader.ItemsList
            .FindAll(x => x.dialogueId == dialogue.key)
            .OrderBy(s => s.seq)
            .ToList();

        // 333, 444 순서로 들어가서 잘못 나옴..

        NextLine();
    }

    public void SelectOption(DialogueOptionEntity selectedOption)
    {
        // TODO : 선택지 선택 후 처리
        optionContainer.SetActive(false);
        NextLine(selectedOption);
    }

    private void DialogueClick()
    {
        if (textSlot.IsPlaying)
            textSlot.Skip();
        else
        {
            if (IsOptionExist())
                ShowOptions();
            else
                NextLine();
        }
    }

    private void NextLine(DialogueOptionEntity selectedOption = null)
    {
        if (selectedOption != null)
            this.selectedOption = selectedOption;
        int lineIndex = GetNextLineIndex(currentLineIndex, this.selectedOption?.key ?? 0);
        DialogueLineEntity line = IsLineExist(lineIndex) ? lines[lineIndex] : null;
        if (line == null)
        {
            // TODO : 대화가 종료되고 무언가 이후 처리가 필요하다면 실행
            // 예를 들면 상인과 거래 UI 팝업을 열어준다던지..
            Close();
            return;
        }

        currentLineIndex = lineIndex;
        characterSlot.Hide();
        textSlot.SetData(this, line, characterName);
        textSlot.Play();

        Sprite sprite = Managers.Resource.Load<Sprite>(line.portraitSpriteName);
        if (sprite)
            characterSlot.Show(sprite, line.portraitPositionType);
        else
            characterSlot.Hide();
    }

    private int GetNextLineIndex(int lineIndex, int optionId = 0)
    {
        if (lineIndex >= lines.Count) return -1;

        if (lineIndex > -1)
        {
            DialogueLineEntity line = lines[lineIndex];
            if (line.seq > sequence && line.optionId == optionId)
            {
                sequence = line.seq;
                return lineIndex;
            }

            if (line.seq > sequence && line.optionId == 0 && optionId > 0)
            {
                // 선택지를 통한 접근 이지만 더 이상 선택지가 없는 경우
                selectedOption = null;
                sequence = line.seq;
                return lineIndex;
            }
        }

        // 재귀호출
        return GetNextLineIndex(lineIndex + 1, optionId);
    }

    private bool IsLineExist(int lineIndex)
    {
        return lineIndex >= 0 && lineIndex < lines.Count;
    }

    private bool IsOptionExist()
    {
        if (currentLineIndex >= lines.Count || currentLineIndex < 0) return false;

        DialogueLineEntity line = lines[currentLineIndex];
        return line.options.Count > 0;
    }

    public void ShowOptions()
    {
        DialogueLineEntity line = lines[currentLineIndex];
        if (line.options.Count <= 0) return;

        SetOrAddOptionSlots(line.options);
        optionContainer.SetActive(true);
    }

    private void SetOrAddOptionSlots(List<int> optionIds)
    {
        var loader = Managers.DB.DialogueOptionEntityLoader;
        int index = 0;
        for (; index < optionIds.Count; index++)
        {
            int optionId = optionIds[index];
            DialogueOptionEntity optionEntity = loader.GetByKey(optionId);
            if (optionSlots.Count > index)
            {
                optionSlots[index].SetData(this, optionEntity);
            }
            else
            {
                GameObject slotGo = Managers.Resource.Instantiate("UIDialogueOptionSlot", optionSlotParent);
                UIDialogueOptionSlot slot = slotGo.GetComponent<UIDialogueOptionSlot>();
                slot.SetData(this, optionEntity);
                optionSlots.Add(slot);
            }
            optionSlots[index].gameObject.SetActive(true);
        }

        if (index < optionSlots.Count)
        {
            for (; index < optionSlots.Count; index++)
            {
                optionSlots[index].Clear();
                optionSlots[index].gameObject.SetActive(false);
            }
        }

        optionContainer.SetActive(true);
    }
}