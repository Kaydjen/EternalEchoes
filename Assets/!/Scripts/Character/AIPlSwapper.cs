using UnityEngine;

public static class AIPlSwapper
{
    #region PUBLIC
    /// <summary>
    /// When a character was changed, we should update its Manual and AI controls
    /// </summary>
    public static void UpdateControls()
    {
        CheckNull.Camera();
        CameraSwitcher.Instance?.UpdateControls();
    }
    /// <summary> 
    ///  Activate Manual controls on currentValue character (and disable AI)
    /// </summary>
    public static void ActivateManualControl()
    {
        CheckNull.Player();
        EnabledManual(true);
        EnabledAI(false);

        Transform player = PlayerCore.Instance?.transform;
        if (player.TryGetComponent(out GameplayModeSwitcher switcher)) 
            switcher.ManualMode();
        else Debug.Log($"{nameof(GameplayModeSwitcher)} is null in {nameof(AIPlSwapper)}");
    }
    /// <summary>
    ///  Activate AI controls on currentValue character  (and disable Manual)
    /// </summary>
    public static void ActivateAIControl()
    {
        CheckNull.Player();
        EnabledManual(false);
        EnabledAI(true);

        Transform player = PlayerCore.Instance?.transform;
        if (player.TryGetComponent(out GameplayModeSwitcher switcher))
            switcher.AIMode();
        else Debug.Log($"{nameof(GameplayModeSwitcher)} is null in {nameof(AIPlSwapper)}");
    }
    /// <summary>
    /// Lock character ability to move or do something (Lock both Manual and AI controls)
    /// </summary>
    public static void LockControl()
    {
        CheckNull.Player();
        PlayerCore.Instance?.transform.GetChild(1).gameObject.SetActive(false);
        PlayerCore.Instance?.transform.GetChild(2).gameObject.SetActive(false);
    }
    /// <summary>
    /// Unlock character ability to move or do something (Unlock Manual or AI controls)
    /// </summary>
    public static void UnlockControl()
    {
        CheckNull.Camera();
        CameraSwitcher.Instance?.ManageControl();
    }
    /// <summary>
    /// Enable or disable Manual control of currentValue character
    /// </summary>
    /// <param name="state">True - enable, False - disable</param>
    public static void EnabledManual(bool state) => PlayerCore.Instance.transform.GetChild(1).gameObject.SetActive(state);
    /// <summary>
    /// Enable or disable AI control of currentValue character
    /// </summary>
    /// <param name="state">True - enable, False - disable</param>
    public static void EnabledAI(bool state) => PlayerCore.Instance.transform.GetChild(2).gameObject.SetActive(state);
    public static void SubscribeOnCameraSwitcher()
    {
        CheckNull.Camera();
        CameraSwitcher.OnFPV_Enable?.AddListener(ActivateManualControl);
        CameraSwitcher.OnIsometricV_Enable?.AddListener(ActivateManualControl);
        CameraSwitcher.OnTopDownV_Enable?.AddListener(ActivateAIControl);
    }
    public static void UnsubscribeOnCameraSwitcher()
    {
        CheckNull.Camera();
        CameraSwitcher.OnFPV_Enable?.RemoveListener(ActivateManualControl);
        CameraSwitcher.OnIsometricV_Enable?.RemoveListener(ActivateManualControl);
        CameraSwitcher.OnTopDownV_Enable?.RemoveListener(ActivateAIControl);
    }
    #endregion
}
