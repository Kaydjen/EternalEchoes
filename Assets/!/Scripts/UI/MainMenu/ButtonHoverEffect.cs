using UnityEngine.UI;
using UnityEngine;

public class ButtonHoverEffect : MonoBehaviour
{
    [SerializeField] private Image _mainIcon;
    [SerializeField] private Image _iconHighlighted;
    [SerializeField] private Image _iconPressed;
    private Image _icon;
    public void OnPointerEnter()
    {
        _icon.sprite = _iconHighlighted.sprite;
        _icon.color = _iconHighlighted.color;
    }
    public void OnPointerExit()
    {
        _icon.sprite = _mainIcon.sprite;
        _icon.color = _mainIcon.color;
    }
    private void Awake()
    {
        _icon = GetComponent<Image>();
        OnPointerExit();
    }
}
