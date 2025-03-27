using UnityEngine;

public class RotateMenuOnCharacter : MonoBehaviour
{
    public Transform targetC;  // Объект, на который нужно навестись
    public Transform childB;   // Дочерний объект B (должен быть дочерним к A в иерархии)

    void Update()
    {
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