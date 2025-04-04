using System;
using UnityEngine;

[RequireComponent(typeof(ParametersManager))]
public class PlayerCore : MonoBehaviour
{
    public static PlayerCore Instance;
    [NonSerialized] public ParametersManager Component;
    private void Awake() => Component = GetComponent<ParametersManager>();

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
        }
        GameEvents.OnCharacterChange?.Invoke();
    }
}
