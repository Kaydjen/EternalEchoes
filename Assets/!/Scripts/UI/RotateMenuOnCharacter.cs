using UnityEngine;
// :)
public class RotateMenuOnCharacter : MonoBehaviour, IUpdate
{
    [SerializeField] private Transform _canvas; // child obj
    private Transform _character;  // Target the menu will rotate to
    private Transform _pos; // the current position of menu 
    #region Update
    public void PerformInitialUpdate() 
    {
        transform.position = _pos.position;

        Vector3 directionToTarget = _character.position - _canvas.position;
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
    public void PerformPreUpdate() { }
    public void PerformUpdate() { }
    public void PerformFinalUpdate() { }
    public void PerformLateUpdate() { }
    #endregion
    #region Registration
    private void RegisterUpdate()
    {
        Updater.Instance?.RegisterUpdate(this, Updater.UpdateType.InitialUpdate);
    }
    private void UnregisterUpdate()
    {
        Updater.Instance?.UnregisterUpdate(this, Updater.UpdateType.InitialUpdate);
    }
    private void Register() { }
    #endregion
    public void Enable(Transform target, Transform pos)
    {
        _character = target;
        _pos = pos;
        RegisterUpdate();
    }
    public void Disable()
    {
        UnregisterUpdate();
    }
}
