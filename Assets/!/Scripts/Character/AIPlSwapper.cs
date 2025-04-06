using UnityEngine;

public class AIPlSwapper : MonoBehaviour
{
    #region PUBLIC METHODS
    /// <summary>
    /// When a character was changed, we should update its Manual and AI controls
    /// </summary>
    public static void UpdateControls()
    {
        CameraSwitcher.Instance.UpdateControls();
    }
    /// <summary> 
    ///  Activate Manual controls on currentValue character (and disable AI)
    /// </summary>
    public static void ActivateManualControl()
    {
        if(PlayerCore.Instance == null) Debug.Log(" Is null  PlayerCore.Instance");
        EnabledManual(true);
        EnabledAI(false);
        PlayerCore.Instance.transform.GetComponent<GameplayModeSwitcher>().ManualMode();
    }
    /// <summary>
    ///  Activate AI controls on currentValue character  (and disable Manual)
    /// </summary>
    public static void ActivateAIControl()
    {
        EnabledManual(false);
        EnabledAI(true);
        PlayerCore.Instance.transform.GetComponent<GameplayModeSwitcher>().AIMode();
    }
    /// <summary>
    /// Lock character ability to move or do something (Lock both Manual and AI controls)
    /// </summary>
    public static void LockControl()
    {
        PlayerCore.Instance.transform.GetChild(1).gameObject.SetActive(false);
        PlayerCore.Instance.transform.GetChild(1).gameObject.SetActive(false);
    }
    /// <summary>
    /// Unlock character ability to move or do something (Unlock Manual or AI controls)
    /// </summary>
    public static void UnlockControl()
    {
        CameraSwitcher.Instance.ManageControl();
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
    #endregion
    #region PRIVATE METHODS
    #endregion
    #region MONO METHODS
    private void OnEnable()
    {
        CameraSwitcher.OnFPV_Enable.AddListener(ActivateManualControl);
        CameraSwitcher.OnIsometricV_Enable.AddListener(ActivateManualControl);
        CameraSwitcher.OnTopDownV_Enable.AddListener(ActivateAIControl);
    }
    private void OnDisable()
    {
        CameraSwitcher.OnFPV_Enable.RemoveListener(ActivateManualControl);
        CameraSwitcher.OnIsometricV_Enable.RemoveListener(ActivateManualControl);
        CameraSwitcher.OnTopDownV_Enable.RemoveListener(ActivateAIControl);
    }
    #endregion
}
