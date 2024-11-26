using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIManager : IManager
{
    private Dictionary<string, UIPopupBase> popupDict = new Dictionary<string, UIPopupBase>();
    private int popupOrder = 10;
    private UISceneBase currentSceneUI;

    private GameObject root;
    public GameObject Root
    {
        get
        {
            root = GameObject.Find("@UI_Root");
            if (root == null)
                root = new GameObject { name = "@UI_Root" };
            return root;
        }
    }

    public void Init()
    {
        if (root != null)
        {
            Managers.Resource.Destroy(root);
            root = null;
        }
        if (GameObject.FindObjectOfType<EventSystem>() == null)
        {
            GameObject obj = new GameObject("EventSystem");
            obj.AddComponent<EventSystem>();
            obj.AddComponent<StandaloneInputModule>();
        }
    }

    public void Clear()
    {
        foreach (var popup in popupDict.Values)
            Managers.Resource.Destroy(popup.gameObject);
        popupDict.Clear();
        if (root != null)
        {
            Managers.Resource.Destroy(root);
            root = null;
        }
    }

    public T LoadSceneUI<T>() where T : UISceneBase
    {
        if (currentSceneUI != null)
        {
            currentSceneUI.Close();
            currentSceneUI = null;
        }
        string name = typeof(T).Name;
        GameObject go = Managers.Resource.Instantiate($"UI/Scene/{name}", Root.transform);
        if (go == null)
        {
            Debug.LogError("Failed to load scene UI : " + typeof(T).Name);
            return null;
        }
        go.name = name;
        currentSceneUI = go.GetComponent<T>();
        currentSceneUI.Open();

        return currentSceneUI as T;
    }
    public T GetCurrentSceneUI<T>() where T : UISceneBase
    {
        return currentSceneUI as T;
    }

    public T ShowPopupUI<T>() where T : UIPopupBase
    {
        string name = typeof(T).Name;

        if (popupDict.TryGetValue(name, out UIPopupBase popup) == false)
        {
            GameObject go = Managers.Resource.Instantiate(name, Root.transform);
            if (go == null)
            {
                Debug.LogError("Failed to load popup : " + name);
                return null;
            }
            go.name = name;
            popup = go.GetComponent<T>();
            popupDict.Add(name, popup);
        }

        if (popup.IsOpen)
            popupOrder--;

        popup.GetComponent<Canvas>().sortingOrder = popupOrder++;
        popup.Open();

        return popup as T;
    }
    public void ClosePopupUI<T>() where T : UIPopupBase
    {
        if (popupDict.TryGetValue(typeof(T).Name, out UIPopupBase popup))
        {
            popup.Close();
        }
    }
    public void ClosePopupUI(UIPopupBase popup)
    {
        popupOrder--;
        popup.GetComponent<Canvas>().sortingOrder = 0;
        popup.gameObject.SetActive(false);
    }
    public void CloseAllPopup()
    {
        foreach (var popup in popupDict.Values)
        {
            if (popup.IsOpen)
                ClosePopupUI(popup);
        }
    }

    public T FindPopup<T>() where T : UIPopupBase
    {
        if (popupDict.TryGetValue(typeof(T).Name, out UIPopupBase popup))
            if (popup.IsOpen)
                return popup as T;

        return null;
    }
}