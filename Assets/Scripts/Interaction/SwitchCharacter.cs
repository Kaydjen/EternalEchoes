using UnityEngine;

public class SwitchCharacter : MonoBehaviour//, IInteraction
{
    public void Interact()
    {
        GetComponent<PlayerCore>().enabled = true;
        Debug.Log("OPA");
    }
}
