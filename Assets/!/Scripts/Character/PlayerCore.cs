using UnityEngine;

public class PlayerCore : MonoBehaviour
{
    public static PlayerCore Instance;
    private void OnEnable()
    {
        if(Instance == null)
        {
            Instance = this;
            AIPlSwapper.ActivateDirectControl();
        }
        else
        {
            if (Instance == this) return;

            AIPlSwapper.ActivateAIControl();
            Instance.enabled = false;

            Instance = this;
            AIPlSwapper.ActivateNewCharacterControls();
        }
        GameEvents.OnCharacterChange?.Invoke();
    }
}
