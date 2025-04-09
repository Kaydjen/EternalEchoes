using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AIGroupManager : MonoBehaviour, IUpdate
{
    private HashSet<AI> _group = new HashSet<AI>();
    private bool _wasInitialized;
    public void ReturnMembers(HashSet<AI> members)
    {
        foreach (AI ai in members) 
        {
            ai.OnFirstHit?.AddListener(FirstHit);
            _group.Add(ai);
        }
    }
    public void FirstHit(GameObject attacker)
    {
        foreach(AI ai in _group)
        {

        }
    }
    #region Update
    public void PerformInitialUpdate() 
    {
        if(_group.Count == 0)
        {
            Debug.Log($"{nameof(_group.Count)} is 0");
            return;
        }
        foreach(AI ai in _group)
        {
            if(ai == null)
            {
                Debug.Log($"{nameof(ai)} is null");
                continue;
            }
            ai.CheckRules();
        }
    }
    public void PerformPreUpdate() { }
    public void PerformUpdate() { }
    public void PerformFinalUpdate() { }
    public void PerformLateUpdate() { }
    private void RegisterUpdate()
    {
        Updater.Instance?.RegisterUpdate(this, Updater.UpdateType.InitialUpdate);
    }
    private void UnregisterUpdate()
    {
        Updater.Instance?.UnregisterUpdate(this, Updater.UpdateType.InitialUpdate);
    }
    #endregion
    private void OnEnable()
    {
        if(_wasInitialized) RegisterUpdate(); // запрос в менеджер групп для спавна 
        else
        {
            Invoke(nameof(RegisterUpdate), 1f);
            _wasInitialized = true;
        }
    }
    private void OnDisable()
    {
        UnregisterUpdate();
    }
}
