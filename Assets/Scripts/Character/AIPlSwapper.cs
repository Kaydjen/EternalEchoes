using UnityEngine;

public class AIPlSwapper : MonoBehaviour
{
    #region VARIABLES
    #endregion
    #region PUBLIC METHODS
    public static void ActivateNewCharacterControls()
    {
        switch(CameraSwitcher.CurrentView) // TODO: тут такая себе система, так что если будет время, нужно будет как-то переделать
        {
            case CameraSwitcher.View.FPV:
                ActivateDirectControl();
                CameraSwitcher.OnFPV_Enable.Invoke();
                break;
            case CameraSwitcher.View.IsometricV:
                ActivateDirectControl();
                CameraSwitcher.OnIsometricV_Enable.Invoke();
                break;
            case CameraSwitcher.View.TopDownV:
                ActivateAIControl();
                CameraSwitcher.OnTopDownV_Enable.Invoke();
                break;
        }
    }
    public static void ActivateDirectControl()
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
