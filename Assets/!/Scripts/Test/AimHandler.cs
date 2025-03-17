using UnityEngine;

public class AimHandler : MonoBehaviour, IGameplayModeSwitcher
{
    #region VARIABLES
    public IAim aim;
    #endregion
    #region PUBLIC METHODS
    public void StartAim()
    {
        aim.StartAim();
    }
    public void StopAim()
    {
        aim.StopAim();
    }
    #endregion
    #region MONOBEHAVIOUR
    private void Awake()
    {
        if (!TryGetComponent(out aim)) Debug.Log("Error");
    }
    public void ForManualMode()
    {
        InputHandler.OnEnterAimingMode.AddListener(StartAim);
        InputHandler.OnExitAimingMode.AddListener(StopAim);
    }

    public void ForAIMode()
    {
        InputHandler.OnEnterAimingMode.RemoveListener(StartAim);
        InputHandler.OnExitAimingMode.RemoveListener(StopAim);
    }
    #endregion
}
