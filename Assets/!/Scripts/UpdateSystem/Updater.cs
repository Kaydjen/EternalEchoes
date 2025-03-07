using System.Collections.Generic;
using UnityEngine;

[ComponentInfo("Custom Updater", "It replace Unity's Update method my own logic based on HashSets")]
public class Updater : MonoBehaviour
{
    private HashSet<IUpdate> InitialUpdateQueue = new HashSet<IUpdate>();
    private HashSet<IUpdate> PreUpdateQueue = new HashSet<IUpdate>();
    private HashSet<IUpdate> UpdateQueue = new HashSet<IUpdate>();
    private HashSet<IUpdate> FinalUpdateQueue = new HashSet<IUpdate>();
    private HashSet<IUpdate> LateUpdateQueue = new HashSet<IUpdate>();

    private HashSet<IUpdate> InitialUpdateAddQueue = new HashSet<IUpdate>();
    private HashSet<IUpdate> PreUpdateAddQueue = new HashSet<IUpdate>();
    private HashSet<IUpdate> UpdateAddQueue = new HashSet<IUpdate>();
    private HashSet<IUpdate> FinalUpdateAddQueue = new HashSet<IUpdate>();
    private HashSet<IUpdate> LateUpdateAddQueue = new HashSet<IUpdate>();

    private HashSet<IUpdate> InitialUpdateRemovalQueue = new HashSet<IUpdate>();
    private HashSet<IUpdate> PreUpdateRemovalQueue = new HashSet<IUpdate>();
    private HashSet<IUpdate> UpdateRemovalQueue = new HashSet<IUpdate>();
    private HashSet<IUpdate> FinalUpdateRemovalQueue = new HashSet<IUpdate>();
    private HashSet<IUpdate> LateUpdateRemovalQueue = new HashSet<IUpdate>();

    public static Updater Instance { get; set; }
    public enum UpdateType { InitialUpdate, PreUpdate, Update, LateUpdate, FinalUpdate }

    public void Init()
    {
        Instance = this;
    }
    private void Update()
    {
        if (InitialUpdateQueue.Count > 0)
        {
            foreach (IUpdate e in InitialUpdateQueue)
            {
                e.PerformInitialUpdate();
            }
        }
        if (PreUpdateQueue.Count > 0)
        {
            foreach (IUpdate e in PreUpdateQueue)
            {
                e.PerformPreUpdate();
            }
        }
        if (UpdateQueue.Count > 0)
        {
            foreach (IUpdate e in UpdateQueue)
            {
                e.PerformUpdate();
            }
        }
        if (FinalUpdateQueue.Count > 0)
        {
            foreach (IUpdate e in FinalUpdateQueue)
            {
                e.PerformFinalUpdate();
            }
        }
        if (LateUpdateQueue.Count > 0)
        {
            foreach (IUpdate e in LateUpdateQueue)
            {
                e.PerformLateUpdate();
            }
        }
    }
    private void AddUpdatesToQueue(ref HashSet<IUpdate> listOfUpdatesToAdd, ref HashSet<IUpdate> queue)
    {
        if (listOfUpdatesToAdd.Count > 0)
        {
            foreach (IUpdate update in listOfUpdatesToAdd)
            {
                queue.Add(update);
            }
            listOfUpdatesToAdd.Clear();
        }
    }
    private void RemoveUpdatesFromQueue(ref HashSet<IUpdate> listOfUpdatesToRemove, ref HashSet<IUpdate> queue)
    {
        if (listOfUpdatesToRemove.Count > 0)
        {
            foreach(IUpdate update in listOfUpdatesToRemove)
            {
                queue.Remove(update);
            }
            listOfUpdatesToRemove.Clear();
        }
    }
    public void RegisterUpdate(IUpdate script, UpdateType updateType)
    {
        switch (updateType)
        {
            case UpdateType.InitialUpdate:
                InitialUpdateAddQueue.Add(script);
                break;
            case UpdateType.PreUpdate:
                PreUpdateAddQueue.Add(script);
                break;
            case UpdateType.Update:
                UpdateAddQueue.Add(script);
                break;
            case UpdateType.LateUpdate:
                LateUpdateAddQueue.Add(script);
                break;
            case UpdateType.FinalUpdate:
                FinalUpdateAddQueue.Add(script);
                break;
            default:
                Debug.Log("RegisterUpdate something goes wrong");
                break;
        }
        AddUpdatesToQueue(ref InitialUpdateAddQueue, ref InitialUpdateQueue);
        AddUpdatesToQueue(ref PreUpdateAddQueue, ref PreUpdateQueue);
        AddUpdatesToQueue(ref UpdateAddQueue, ref UpdateQueue);
        AddUpdatesToQueue(ref FinalUpdateAddQueue, ref FinalUpdateQueue);
        AddUpdatesToQueue(ref LateUpdateAddQueue, ref LateUpdateQueue);
    }
    public void UnregisterUpdate(IUpdate script, UpdateType updateType)
    {
        switch (updateType)
        {
            case UpdateType.InitialUpdate:
                InitialUpdateRemovalQueue.Add(script);
                break;
            case UpdateType.PreUpdate:
                PreUpdateRemovalQueue.Add(script);
                break;
            case UpdateType.Update:
                UpdateRemovalQueue.Add(script);
                break;
            case UpdateType.LateUpdate:
                LateUpdateRemovalQueue.Add(script);
                break;
            case UpdateType.FinalUpdate:
                FinalUpdateRemovalQueue.Add(script);
                break;
            default:
                Debug.Log("UnregisterUpdate something goes wrong");
                break;
        }
        RemoveUpdatesFromQueue(ref InitialUpdateRemovalQueue, ref InitialUpdateQueue);
        RemoveUpdatesFromQueue(ref PreUpdateRemovalQueue, ref PreUpdateQueue);
        RemoveUpdatesFromQueue(ref UpdateRemovalQueue, ref UpdateQueue);
        RemoveUpdatesFromQueue(ref FinalUpdateRemovalQueue, ref FinalUpdateQueue);
        RemoveUpdatesFromQueue(ref LateUpdateRemovalQueue, ref LateUpdateQueue);
    }
}
/*
 
    #region Update
    public void PerformInitialUpdate()
    {
        throw new System.NotImplementedException();
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
        Updater.Instance.RegisterUpdate(this, Updater.UpdateType.);
    }
    private void UnregisterUpdate()
    {
        Updater.Instance.UnregisterUpdate(this, Updater.UpdateType.);
    }
    #endregion
 
 
 */
/*
 
     #region Update
    public bool NeedsInitialUpdate
    {
        get { return false; }
        set { }
    }
    public bool NeedsPreUpdate
    {
        get { return false; }
        set { }
    }
    public bool NeedsUpdate
    {
        get { return false; }
        set { }
    }
    public bool NeedsFinalUpdate
    {
        get { return false; }
        set { }
    }
    public bool NeedsLateUpdate
    {
        get { return false; }
        set { }
    }
    public void PerformInitialUpdate()
    {
        throw new System.NotImplementedException();
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
    #endregion
 
 
 */
/*    void Awake()
        {
            Instance = this;
            MonoBehaviour[] scripts = FindObjectsOfType<MonoBehaviour>();
            foreach (MonoBehaviour script in scripts)
            {
                if (script is IUpdate)
                {
                    if ((script as IUpdate).NeedsInitialUpdate)
                    {
                        InitialUpdateQueue.Add((IUpdate)script);
                    }
                    if ((script as IUpdate).NeedsPreUpdate)
                    {
                        PreUpdateQueue.Add((IUpdate)script);
                    }
                    if ((script as IUpdate).NeedsUpdate)
                    {
                        UpdateQueue.Add((IUpdate)script);
                    }
                    if ((script as IUpdate).NeedsLateUpdate)
                    {
                        LateUpdateQueue.Add((IUpdate)script);
                    }
                    if ((script as IUpdate).NeedsFinalUpdate)
                    {
                        FinalUpdateQueue.Add((IUpdate)script);
                    }
                }
            }
        }*/