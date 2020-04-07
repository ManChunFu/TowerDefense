using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MouseOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Text _text = null;
    [SerializeField] private Color _mouseOverColor = default;
    [SerializeField] private int _fontSizeMouseOver = 40; 

    private Color _startColor = default;
    private int _startFontSize = 30;

    private void Awake()
    {
        if (_text == null)
            Debug.Log ("Missing reference of Text game object.");
        _startColor = _text.color;
        _startFontSize = _text.fontSize;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _text.color = _mouseOverColor;
        _text.fontSize = _fontSizeMouseOver;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        _text.color = _startColor;
        _text.fontSize = _startFontSize;
    }
}
