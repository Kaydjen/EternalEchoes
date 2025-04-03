using UnityEngine.UI;
using UnityEngine;

public class IconHoverEffect : MonoBehaviour
{
    [SerializeField] private Image _mainIcon;
    [SerializeField] private Color _mainIconColor;
    [SerializeField] private Color _iconHighlightedColor;
    [SerializeField] private Color _iconPressedColor;
    public void OnPointerEnter()
    {
        _mainIcon.color = _iconHighlightedColor;
    }
    public void OnPointerExit()
    {
        _mainIcon.color = _mainIconColor;
    }
    private void Awake()
    {
        OnPointerExit();
        _mainIcon.gameObject.SetActive(true);
    }
}
