using UnityEngine;
using static UnityEngine.GraphicsBuffer;

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


/*
         Vector3 toTarget = (PlayerCore.Instance.transform.position - transform.position).normalized;
        float angle = Vector3.Angle(_menu.transform.forward, toTarget);

        if (angle > _maxAngle)
        {
            Vector3 limitedDirection = Vector3.RotateTowards(_menu.transform.forward, toTarget, Mathf.Deg2Rad * _maxAngle, 0);
            transform.rotation = Quaternion.LookRotation(limitedDirection);
        }
 
 
 */