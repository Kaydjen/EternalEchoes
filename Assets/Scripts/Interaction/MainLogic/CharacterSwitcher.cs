using UnityEngine;

public class CharacterSwitcher : MonoBehaviour
{
    private void Start()
    {
        InputHandler.OnSwitchCharacter.AddListener(Switch);       
    }
    private void Switch()
    {
        Debug.Log("Nu ono raboraet");
        if (Hover.HitedCollider != null && Hover.HitedCollider.TryGetComponent(out ISwitchCharacter link)) link.Switch();
    }
}
