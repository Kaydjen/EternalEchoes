using UnityEngine;

public class CharacterSwitchable : MonoBehaviour, ISwitchCharacter
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
