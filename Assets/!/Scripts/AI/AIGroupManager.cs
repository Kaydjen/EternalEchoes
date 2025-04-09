using System.Collections.Generic;
using UnityEngine;

public class AIGroupManager : MonoBehaviour, IUpdate
{
    private HashSet<AI> _group = new();
    private HashSet<Transform> _targets = new();
    private bool _wasInitialized;

    public void AddNewTarget(Transform target)
    {
        _targets.Add(target);
        UpdateTargets();
    }
    public void ReturnMembers(HashSet<AI> members)
    {
        foreach (AI ai in members) 
        {
            ai.OnFirstHit?.AddListener(FirstHit);
            _group.Add(ai);
        }
        UpdateTargets();
    }
    private void FirstHit(GameObject attacker)
    {
        foreach(AI ai in _group)
        {

        }
    }
    public void StartLogic()
    {
        if(!_wasInitialized)
        {
            RegisterUpdate();
            _wasInitialized = true;
        }
    }
    private void UpdateTargets()
    {
        foreach(AI ai in _group)
            ai.SetNewTargetList(_targets);
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
    private void OnDisable()
    {
        UnregisterUpdate();
    }
    private void OnDestroy()
    {
        UnregisterUpdate();
    }
}




