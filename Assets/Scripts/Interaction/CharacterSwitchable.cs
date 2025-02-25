using UnityEngine;

public class CharacterSwitchable : MonoBehaviour, ISwitchCharacter
{
    public void Switch()
    {
        GetComponent<PlayerCore>().enabled = true;
        Debug.Log("OPA");
    }
}
