using UnityEngine;

public class ShowClue : MonoBehaviour, IInteraction, IUpdate
{
    public void HoverEnter()
    {
        this.transform.GetChild(0).gameObject.SetActive(true);
        Debug.Log("Enter");
        this.transform.GetChild(0).transform.localRotation = Quaternion.LookRotation(this.transform.GetChild(0).transform.position - Camera.main.transform.position);
        Updater.Instance.RegisterUpdate(this, Updater.UpdateType.LateUpdate);
    }

    public void HoverExit()
    {
        this.transform.GetChild(0).gameObject.SetActive(false);
        Debug.Log("Exit");
        Updater.Instance.UnregisterUpdate(this, Updater.UpdateType.LateUpdate);
    }
    public void PerformInitialUpdate()
    {
        throw new System.NotImplementedException();
    }
    public void PerformLateUpdate()
    {
        this.transform.GetChild(0).transform.localRotation = Quaternion.LookRotation(this.transform.GetChild(0).transform.position - Camera.main.transform.position);
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
}
