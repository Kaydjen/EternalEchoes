using UnityEngine;

[ComponentInfo("", "Включати в кiнцi")]
public class RotationIsometric : MonoBehaviour, IUpdate
{
    #region VARIABLES
    private Transform _player;
    #endregion
    #region METHODS
    private void ChangePlayer()
    {
        _player = PlayerCore.Instance.transform.GetChild(Constants.Player.BOTH).transform;
    }
    #endregion
    #region MONO METHODS
    private void OnEnable()
    {
        CameraSwitcher.OnIsometricV_Enable.AddListener(RegisterUpdate);
        CameraSwitcher.OnFPV_Enable.AddListener(UnregisterUpdate);

        GameEvents.OnCharacterChange.AddListener(ChangePlayer);
    }
    private void OnDisable()
    {
        CameraSwitcher.OnIsometricV_Enable.RemoveListener(RegisterUpdate);
        CameraSwitcher.OnFPV_Enable.RemoveListener(UnregisterUpdate);

        UnregisterUpdate();

        GameEvents.OnCharacterChange.RemoveListener(ChangePlayer);
    }
    #endregion
    #region Update
    public void PerformInitialUpdate()
    {
        throw new System.NotImplementedException();
    }
    public void PerformPreUpdate()
    {
        _player.localRotation = this.transform.localRotation;
    }
    public void PerformUpdate()
    {
        throw new System.NotImplementedException();
    }
    public void PerformFinalUpdate()
    {
        throw new System.NotImplementedException();
    }
    public void PerformLateUpdate()
    {
        throw new System.NotImplementedException();
    }
    private void RegisterUpdate()
    {
        Updater.Instance.RegisterUpdate(this, Updater.UpdateType.PreUpdate);
    }
    private void UnregisterUpdate()
    {
        Updater.Instance.UnregisterUpdate(this, Updater.UpdateType.PreUpdate);
    }
    #endregion
}
