using UnityEngine;

public class AIPlSwapper : MonoBehaviour
{
    #region VARIABLES
    #endregion
    #region PUBLIC METHODS
    public static void ActivatePlayerControl()
    {
        PlayerCore.Instance.transform.GetChild(1).gameObject.SetActive(true);
        PlayerCore.Instance.transform.GetChild(2).gameObject.SetActive(false);
    }
    public static void ActivateAIControl()
    {
        PlayerCore.Instance.transform.GetChild(1).gameObject.SetActive(false);
        PlayerCore.Instance.transform.GetChild(2).gameObject.SetActive(true);
    }
    #endregion
    #region PRIVATE METHODS
    #endregion
    #region MONO METHODS
    private void OnEnable()
    {
        CameraSwitcher.OnFPV_Enable.AddListener(ActivatePlayerControl);
        CameraSwitcher.OnIsometricV_Enable.AddListener(ActivatePlayerControl);
        CameraSwitcher.OnTopDownV_Enable.AddListener(ActivateAIControl);
    }
    private void OnDisable()
    {
        CameraSwitcher.OnFPV_Enable.RemoveListener(ActivatePlayerControl);
        CameraSwitcher.OnIsometricV_Enable.RemoveListener(ActivatePlayerControl);
        CameraSwitcher.OnTopDownV_Enable.RemoveListener(ActivateAIControl);
    }
    #endregion
}
