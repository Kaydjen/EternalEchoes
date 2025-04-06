using System;
using UnityEngine;

[RequireComponent(typeof(ParametersManager))]
public class PlayerCore : MonoBehaviour
{
    public static PlayerCore Instance;
    [NonSerialized] public ParametersManager Component;
    private void Awake()
    {
        if (Instance == null)
        {
            OnEnable();
            InitEvents.OnFirstCharacterInit?.Invoke();
        }
        Component = GetComponent<ParametersManager>();
    }

    private void OnEnable()
    {
        Debug.Log("OnEnable PlayerCore start");
        if (Instance == null)
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
        Debug.Log("OnEnable PlayerCore end");
        GameEvents.OnCharacterChange?.Invoke();
    }
}
