using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class UIBase : MonoBehaviour
{
    private bool isInit = false;
    protected Dictionary<Type, UnityEngine.Object[]> _objects = new Dictionary<Type, UnityEngine.Object[]>();

    protected virtual bool Init()
    {
        if (isInit) // 한번이라도 초기화 했다면 다시 초기화 하지 않도록 하기.
            return false;
        
        isInit = true;
        return true;
    }

    private void Awake()
    {
        Init();
    }

    public virtual void Open(Defines.UIAnimationType type = Defines.UIAnimationType.None)
    {
        // TODO : Open Animation
    }
    
    public virtual void Close(Defines.UIAnimationType type = Defines.UIAnimationType.None)
    {
        // TODO : Close Animation
    }

    protected void Bind<T>(Type type) where T : UnityEngine.Object
    {
        string[] names = Enum.GetNames(type);
        UnityEngine.Object[] objects = new UnityEngine.Object[names.Length];
        _objects.Add(typeof(T), objects);

        for (int i = 0; i < names.Length; i++)
        {
            if (typeof(T) == typeof(GameObject))
                objects[i] = Utils.FindChild(gameObject, names[i], true);
            else
                objects[i] = Utils.FindChild<T>(gameObject, names[i], true);

            if (objects[i] == null)
                Debug.LogWarning($"Failed to bind({names[i]})");
        }
    }

    protected void BindObject(Type type) { Bind<GameObject>(type); }
    protected void BindImage(Type type) { Bind<Image>(type); }
    protected void BindText(Type type) { Bind<TMP_Text>(type); }
    protected void BindButton(Type type) { Bind<Button>(type); }
    protected void BindToggle(Type type) { Bind<Toggle>(type); }


    protected T Get<T>(int idx) where T : UnityEngine.Object
    {
        UnityEngine.Object[] objects = null;
        if (_objects.TryGetValue(typeof(T), out objects) == false)
            return null;

        return objects[idx] as T;
    }

    protected GameObject GetObject(Enum idx) { return Get<GameObject>((int)(object)idx); }
    protected TMP_Text GetText(Enum idx) { return Get<TMP_Text>((int)(object)idx); }
    protected Button GetButton(Enum idx) { return Get<Button>((int)(object)idx); }
    protected Image GetImage(Enum idx) { return Get<Image>((int)(object)idx); }
    protected Toggle GetToggle(Enum idx) { return Get<Toggle>((int)(object)idx); }

    public static void BindEvent(GameObject go, Action action = null, Action<BaseEventData> dragAction = null, Defines.UIEvent type = Defines.UIEvent.Click)
    {
        UIEventHandler evt = Utils.GetOrAddComponent<UIEventHandler>(go);

        switch (type)
        {
            case Defines.UIEvent.Click:
                evt.OnClickHandler -= action;
                evt.OnClickHandler += action;
                break;
            case Defines.UIEvent.Pressed:
                evt.OnPressedHandler -= action;
                evt.OnPressedHandler += action;
                break;
            case Defines.UIEvent.PointerDown:
                evt.OnPointerDownHandler -= action;
                evt.OnPointerDownHandler += action;
                break;
            case Defines.UIEvent.PointerUp:
                evt.OnPointerUpHandler -= action;
                evt.OnPointerUpHandler += action;
                break;
            case Defines.UIEvent.Drag:
                evt.OnDragHandler -= dragAction;
                evt.OnDragHandler += dragAction;
                break;
            case Defines.UIEvent.BeginDrag:
                evt.OnBeginDragHandler -= dragAction;
                evt.OnBeginDragHandler += dragAction;
                break;
            case Defines.UIEvent.EndDrag:
                evt.OnEndDragHandler -= dragAction;
                evt.OnEndDragHandler += dragAction;
                break;
        }
    }
    public static void UnBindEvent(GameObject go, Action action = null, Action<BaseEventData> dragAction = null, Defines.UIEvent type = Defines.UIEvent.Click)
    {
        UIEventHandler evt = go.GetComponent<UIEventHandler>();
        if (evt == null) return;
        
        switch (type)
        {
            case Defines.UIEvent.Click:
                evt.OnClickHandler -= action;
                break;
            case Defines.UIEvent.Pressed:
                evt.OnPressedHandler -= action;
                break;
            case Defines.UIEvent.PointerDown:
                evt.OnPointerDownHandler -= action;
                break;
            case Defines.UIEvent.PointerUp:
                evt.OnPointerUpHandler -= action;
                break;
            case Defines.UIEvent.Drag:
                evt.OnDragHandler -= dragAction;
                break;
            case Defines.UIEvent.BeginDrag:
                evt.OnBeginDragHandler -= dragAction;
                break;
            case Defines.UIEvent.EndDrag:
                evt.OnEndDragHandler -= dragAction;
                break;
        }
    }
}
