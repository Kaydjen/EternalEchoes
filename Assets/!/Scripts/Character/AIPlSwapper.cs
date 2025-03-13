using UnityEngine;

public class AIPlSwapper : MonoBehaviour
{
    #region VARIABLES
    #endregion
    #region PUBLIC METHODS
    public static void ActivateNewCharacterControls()
    {
        CameraSwitcher.Instance.UpdateControlsOnViewChange();
    }
    public static void ActivateDirectControl()
    {
        ManageDirect(true);
        ManageAI(false);
        PlayerCore.Instance.transform.GetComponent<GameplayModeSwitcher>().DirectMode();
    }
    public static void ActivateAIControl()
    {
        ManageDirect(false);
        ManageAI(true);
        PlayerCore.Instance.transform.GetComponent<GameplayModeSwitcher>().AIMode();
    }
    public static void Reabilitation()
    {
        CameraSwitcher.Instance.ManageControls();
    }
    public static void DisReabilitation()
    {
        PlayerCore.Instance.transform.GetChild(1).gameObject.SetActive(false);
        PlayerCore.Instance.transform.GetChild(1).gameObject.SetActive(false);
    }
    public static void ManageDirect(bool state) => PlayerCore.Instance.transform.GetChild(1).gameObject.SetActive(state);
    public static void ManageAI(bool state) => PlayerCore.Instance.transform.GetChild(2).gameObject.SetActive(state);
    #endregion
    #region PRIVATE METHODS
    #endregion
    #region MONO METHODS
    private void OnEnable()
    {
        CameraSwitcher.OnFPV_Enable.AddListener(ActivateDirectControl);
        CameraSwitcher.OnIsometricV_Enable.AddListener(ActivateDirectControl);
        CameraSwitcher.OnTopDownV_Enable.AddListener(ActivateAIControl);
    }
    private void OnDisable()
    {
        CameraSwitcher.OnFPV_Enable.RemoveListener(ActivateDirectControl);
        CameraSwitcher.OnIsometricV_Enable.RemoveListener(ActivateDirectControl);
        CameraSwitcher.OnTopDownV_Enable.RemoveListener(ActivateAIControl);
    }
    #endregion
}
