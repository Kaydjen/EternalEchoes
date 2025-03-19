using UnityEngine;

public class PlayerCore : MonoBehaviour
{
    public static PlayerCore Instance;
    private void OnEnable()
    {
        if(Instance == null)
        {
            Instance = this;
            AIPlSwapper.ActivateManualControl();
        }
        else
        {
            if (Instance == this) return;

            AIPlSwapper.ActivateAIControl();
            Instance.enabled = false;

            Instance = this;
            AIPlSwapper.UpdateControls();
            Debug.Log("Switched");
        }
        GameEvents.OnCharacterChange?.Invoke();
    }
}
