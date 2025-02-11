using UnityEngine;

[ComponentInfo("", "Для того, щоб гравець дивився куди треба. Поворот залежить вiд камери")]
public class LookOrientation : MonoBehaviour, IUpdate
{
    #region Variables
    public static Vector3 Direction { get; private set; }
    #endregion
    #region Update
    public void PerformInitialUpdate()
    {
        throw new System.NotImplementedException();
    }
    public void PerformPreUpdate()
    {
        Direction = (this.transform.GetChild(0).position - this.transform.position).normalized;
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
