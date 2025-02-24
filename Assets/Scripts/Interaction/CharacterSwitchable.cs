using UnityEngine;

public class CharacterSwitchable : MonoBehaviour, IInteraction
{
    public void HoverEnter()
    {
        GetComponent<PlayerCore>().enabled = true;
        Debug.Log("OPA");
    }

    public void HoverExit()
    {
        throw new System.NotImplementedException();
    }
}
