using UnityEngine;

public class ShowClue : MonoBehaviour, IInteraction
{
    public void HoverEnter()
    {
        this.transform.GetChild(0).gameObject.SetActive(true);
        Debug.Log("Enter");
    }

    public void HoverExit()
    {
        this.transform.GetChild(0).gameObject.SetActive(false);
        Debug.Log("Exit");
    }
}
