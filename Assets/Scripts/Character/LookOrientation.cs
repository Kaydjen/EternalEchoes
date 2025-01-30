using UnityEngine;

public class LookOrientation : MonoBehaviour, IUpdate
{
    #region Variables
    private Transform _player;
    #endregion
    #region Update
    public void PerformInitialUpdate()
    {
        throw new System.NotImplementedException();
    }
    public void PerformPreUpdate()
    {
        throw new System.NotImplementedException();
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
        this.transform.position = _player.position;
    }
    private void RegisterUpdate()
    {
        Updater.Instance.RegisterUpdate(this, Updater.UpdateType.LateUpdate);
    }
    private void UnregisterUpdate()
    {
        Updater.Instance.UnregisterUpdate(this, Updater.UpdateType.LateUpdate);
    }
    #endregion
    #region Methods
    public void Init()
    {
        _player = PlayerCore.Instance.transform;
    }
    #endregion
    #region OnEvent
    private void OnEnable()
    {
        RegisterUpdate();
    }
    private void OnDisable()
    {
        UnregisterUpdate();
    }
    #endregion
}
