using UnityEngine;

public class RotateMenuOnCharacter : MonoBehaviour, IUpdate
{
    public Transform targetC;  // Объект, на который нужно навестись
    public Transform childB;   // Дочерний объект B (должен быть дочерним к A в иерархии)
    private Transform _pos;
    #region Update
    public void PerformInitialUpdate()
    {
        transform.position = _pos.position;

        Vector3 directionToTarget = targetC.position - childB.position;
        directionToTarget.y = 0;

        if (directionToTarget.sqrMagnitude > 0.001f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.Euler(0, targetRotation.eulerAngles.y, 0),
                Time.deltaTime * 5
            );
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
    public void Enable(Transform traget, Transform pos)
    {
        targetC = traget;
        _pos = pos;
        RegisterUpdate();
    }
    public void Disable()
    {
        UnregisterUpdate();
    }
}
