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
        if (Hover.HitInfo.collider != null && Hover.HitInfo.collider.TryGetComponent(out ISwitchCharacter link)) link.Switch();
    }
}
