using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; // or use TMPro if that’s what you’re using

public class ButtonTextHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Text label;            // swap to TextMeshProUGUI if needed
    [SerializeField] private Color hoverColor = Color.yellow;
    [SerializeField] private Color normalColor = Color.white;

    private void Awake()
    {
        if (!label) label = GetComponentInChildren<Text>(); // auto-grab if not set
        normalColor = label.color;                          // remember starting color
    }

    public void OnPointerEnter(PointerEventData eventData) => label.color = hoverColor;
    public void OnPointerExit(PointerEventData eventData) => label.color = normalColor;

    public Color GetNormalColor()
    {
        return normalColor;
    }

    public Color GetHoverColor()
    {
        return hoverColor;
    }
}