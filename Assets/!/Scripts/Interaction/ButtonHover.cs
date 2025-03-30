using UnityEngine;

public class ButtonHover : MonoBehaviour, IHover
{
    public void HoverEnter()
    {
        this.GetComponentInParent<Tab>().MainIconHighlighted.gameObject.SetActive(true);
    }
    public void HoverExit()
    {
        this.GetComponentInParent<Tab>().MainIconHighlighted.gameObject.SetActive(false);
    }
}
