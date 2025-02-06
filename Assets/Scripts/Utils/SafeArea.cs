using UnityEngine;
using UnityEngine.UI;

public class SafeArea : MonoBehaviour
{
    enum BarPosition
    {
        Top,
        Bottom,
        Left,
        Right
    }

    private RectTransform rect;

    private RectTransform topBlackBar;
    private RectTransform bottomBlackBar;
    private RectTransform leftBlackBar;
    private RectTransform rightBlackBar;

    [SerializeField] private bool useBlackBar = true;

    private void Awake()
    {

        ScreenRatio();

        rect = GetComponent<RectTransform>();
        Rect safeArea = Screen.safeArea;
        Vector2 minAnchor = safeArea.position;
        Vector2 maxAnchor = minAnchor + safeArea.size;

        minAnchor.x /= Screen.width;
        minAnchor.y /= Screen.height;
        maxAnchor.x /= Screen.width;
        maxAnchor.y /= Screen.height;

        rect.anchorMin = minAnchor;
        rect.anchorMax = maxAnchor;

        if (useBlackBar)
        {
            RectTransform parent = transform.parent.GetComponent<RectTransform>();
            AnchorBlackBar(parent, rect, BarPosition.Top);
            AnchorBlackBar(parent, rect, BarPosition.Bottom);
            AnchorBlackBar(parent, rect, BarPosition.Left);
            AnchorBlackBar(parent, rect, BarPosition.Right);
        }
    }

    public void SetUseBlackBar(bool use)
    {
        useBlackBar = use;
    }


    private RectTransform GetBlackBar(RectTransform parent, BarPosition barPos)
    {
        return barPos switch
        {
            BarPosition.Top => topBlackBar ??= CreateBlackBar(parent, barPos),
            BarPosition.Bottom => bottomBlackBar ??= CreateBlackBar(parent, barPos),
            BarPosition.Left => leftBlackBar ??= CreateBlackBar(parent, barPos),
            BarPosition.Right => rightBlackBar ??= CreateBlackBar(parent, barPos),
            _ => null
        };
    }

    private RectTransform CreateBlackBar(RectTransform parent, BarPosition barPos)
    {
        GameObject blackBar = new GameObject($"{barPos}BlackBar");
        blackBar.transform.SetParent(parent);
        Image image = blackBar.GetOrAddComponent<Image>();
        image.color = Color.black;
        image.raycastTarget = true;
        return blackBar.GetOrAddComponent<RectTransform>();
    }

    private void AnchorBlackBar(RectTransform canvas, RectTransform safeArea, BarPosition barPos)
    {
        RectTransform blackBarRT = GetBlackBar(canvas, barPos);
        switch (barPos)
        {
            case BarPosition.Top:
                blackBarRT.anchorMin = new Vector2(0, safeArea.anchorMax.y);
                blackBarRT.anchorMax = new Vector2(1, 1);
                break;
            case BarPosition.Bottom:
                blackBarRT.anchorMin = new Vector2(0, 0);
                blackBarRT.anchorMax = new Vector2(1, safeArea.anchorMin.y);
                break;
            case BarPosition.Left:
                blackBarRT.anchorMin = new Vector2(0, 0);
                blackBarRT.anchorMax = new Vector2(safeArea.anchorMin.x, 1);
                break;
            case BarPosition.Right:
                blackBarRT.anchorMin = new Vector2(safeArea.anchorMax.x, 0);
                blackBarRT.anchorMax = new Vector2(1, 1);
                break;
        }

        blackBarRT.offsetMin = Vector3.zero;
        blackBarRT.offsetMax = Vector3.zero;
        blackBarRT.localScale = Vector3.one;
    }
    
    private void ScreenRatio()
    {
        float screenRatio = (float)Screen.width / Screen.height;
        float safeAreaRatio = Screen.safeArea.width / Screen.safeArea.height;
        Debug.Log($"Screen Ratio : {screenRatio}({Screen.width}x{Screen.height}), SafeArea Ratio : {safeAreaRatio}({Screen.safeArea.width}x{Screen.safeArea.height})");
    }


}