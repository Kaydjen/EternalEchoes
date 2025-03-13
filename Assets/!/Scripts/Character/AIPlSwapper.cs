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
        PlayerCore.Instance.transform.GetChild(1).gameObject.SetActive(true);
        PlayerCore.Instance.transform.GetChild(2).gameObject.SetActive(false);
        PlayerCore.Instance.transform.GetComponent<GameplayModeSwitcher>().DirectMode();
    }
    public static void ActivateAIControl()
    {
        PlayerCore.Instance.transform.GetChild(1).gameObject.SetActive(false);
        PlayerCore.Instance.transform.GetChild(2).gameObject.SetActive(true);
        PlayerCore.Instance.transform.GetComponent<GameplayModeSwitcher>().AIMode();
    }
    public static void Reabilitation()
    {
        CameraSwitcher.Instance.IDK();
    }
    public static void DisReabilitation()
    {
        PlayerCore.Instance.transform.GetChild(1).gameObject.SetActive(false);
        PlayerCore.Instance.transform.GetChild(1).gameObject.SetActive(false);
    }
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
