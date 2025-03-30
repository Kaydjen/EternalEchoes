using UnityEngine;

public class ButtonHover : MonoBehaviour, IHover
{
    private GameObject _icon;
    private void Start()
    {
        _icon = this.GetComponentInParent<Tab>().MainIconHighlighted.gameObject;
    }
    public void HoverEnter()
    {
        _icon.SetActive(true);
    }
    public void HoverExit()
    {
        _icon.SetActive(false);
    }
}
