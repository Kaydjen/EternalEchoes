using UnityEngine;

public class AimDirection : MonoBehaviour, IUpdate
{
    public static Transform Direction { get; private set; }
    [SerializeField] private Transform _lookOrientation;
    private Transform _camera;
    #region Update
    public void PerformInitialUpdate()
    {
        throw new System.NotImplementedException();
    }
    public void PerformPreUpdate()
    {
        transform.localRotation = _camera.localRotation;
    }
    public void PerformUpdate()
    {
        transform.localRotation = _lookOrientation.localRotation;
    }
    public void PerformFinalUpdate()
    {
        throw new System.NotImplementedException();
    }
    public void PerformLateUpdate()
    {
        throw new System.NotImplementedException();
    }
    private void RegisterFPVUpdate()
    {
        Updater.Instance.RegisterUpdate(this, Updater.UpdateType.PreUpdate);
        Updater.Instance.UnregisterUpdate(this, Updater.UpdateType.Update);
    }
    private void RegisterIsometricVUpdate()
    {
        Updater.Instance.UnregisterUpdate(this, Updater.UpdateType.PreUpdate);
        Updater.Instance.RegisterUpdate(this, Updater.UpdateType.Update);
    }
    #endregion
    private void OnEnable()
    {
        RegisterFPVUpdate();
        CameraSwitcher.OnFPV_Enable.AddListener(RegisterFPVUpdate);
        CameraSwitcher.OnIsometricV_Enable.AddListener(RegisterIsometricVUpdate);
        _camera = this.transform.root.GetChild(0);
        Direction = this.transform;
    }
    private void OnDisable()
    {
        RegisterIsometricVUpdate();
    }
}
