using UnityEngine;

public class RotateMenuOnCharacter : MonoBehaviour, IUpdate, IGameplayModeSwitcher
{
    [SerializeField] private Transform _menu;
    [SerializeField] private float _maxAngle = 50f;
    [SerializeField] private float _speed = 50f;
    #region Update
    public void PerformInitialUpdate()
    {
        Vector3 toTarget = (PlayerCore.Instance.transform.position - transform.position).normalized;
        float angle = Vector3.Angle(_menu.transform.forward, toTarget);

        if (angle > _maxAngle)
        {
            Vector3 limitedDirection = Vector3.Lerp(_menu.transform.forward, toTarget, _speed * Time.deltaTime);
            transform.rotation = Quaternion.LookRotation(limitedDirection);
        }
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
        throw new System.NotImplementedException();
    }
    private void RegisterUpdate()
    {
        Updater.Instance.RegisterUpdate(this, Updater.UpdateType.InitialUpdate);
    }
    private void UnregisterUpdate()
    {
        Updater.Instance.UnregisterUpdate(this, Updater.UpdateType.InitialUpdate);
    }
    #endregion
    public void ForManualMode()
    {
        RegisterUpdate();
    }
    public void ForAIMode()
    {

    }
}


/*
         Vector3 toTarget = (PlayerCore.Instance.transform.position - transform.position).normalized;
        float angle = Vector3.Angle(_menu.transform.forward, toTarget);

        if (angle > _maxAngle)
        {
            Vector3 limitedDirection = Vector3.RotateTowards(_menu.transform.forward, toTarget, Mathf.Deg2Rad * _maxAngle, 0);
            transform.rotation = Quaternion.LookRotation(limitedDirection);
        }
 
 
 */