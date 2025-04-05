using UnityEngine;

public class ButtonHoverShop : MonoBehaviour, IHover
{
    private Tab _icon;
    private void Start()
    {
        _icon = this.GetComponent<Tab>();
    }
    public void HoverEnter()
    {
        _icon.MainIcon.gameObject.SetActive(false);
        _icon.MainIconHighlighted.gameObject.SetActive(true);
    }
    public void HoverExit()
    {
        _icon.MainIcon.gameObject.SetActive(true);
        _icon.MainIconHighlighted.gameObject.SetActive(false);
    }
}