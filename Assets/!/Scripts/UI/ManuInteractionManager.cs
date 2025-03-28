using UnityEngine;
using UnityEngine.UI;

public class ManuInteractionManager : MonoBehaviour, IUpdate
{
    #region Update
    public void PerformInitialUpdate()
    {
        if (!Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hitInfo, 10)) return;
        if (!hitInfo.collider.CompareTag("Button")) return;
        if (!hitInfo.collider.TryGetComponent(out Button button)) return;

        Debug.Log("Poniatno");
        button.onClick?.Invoke();        
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
    private void OnEnable()
    {
        RegisterUpdate();
    }
    private void OnDisable()
    {
        UnregisterUpdate();
    }
}