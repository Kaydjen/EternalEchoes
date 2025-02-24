using UnityEngine;

public class CharacterSwitcher : MonoBehaviour
{
    private void Start()
    {
        InputHandler.OnSwitchCharacter.AddListener(Switch);       
    }
    private void Switch()
    {
        Hover.SetNewState<ISwitchCharacter>();
    }
}
