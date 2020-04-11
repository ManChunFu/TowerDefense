using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MouseOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Text m_Text = null;
    [SerializeField] private Color m_MouseOverColor = default;
    [SerializeField] private int m_FontSizeMouseOver = 40; 

    private Color m_StartColor = default;
    private int m_StartFontSize = 30;

    private void Awake()
    {
        if (m_Text == null)
        {
            throw new MissingReferenceException("Missing reference of Text game object.");
        }
        m_StartColor = m_Text.color;
        m_StartFontSize = m_Text.fontSize;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        m_Text.color = m_MouseOverColor;
        m_Text.fontSize = m_FontSizeMouseOver;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        m_Text.color = m_StartColor;
        m_Text.fontSize = m_StartFontSize;
    }
}
