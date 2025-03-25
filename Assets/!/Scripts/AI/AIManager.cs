using UnityEngine;

public class AIManager : MonoBehaviour
{
    #region PRIVATE 
    private void SubscribeOnEvents()
    {
        EnemyRepository.Instance.OnRegister += _ => EnemySpawnEffectPool.Instance.SetEffect(_.transform.position);
        EnemyRepository.Instance.OnUnregister += _ => Debug.Log($"Enemy with ID {_} just died");
    }
    private void UnsubscribeOnEvents()
    {
        EnemyRepository.Instance.OnRegister -= _ => EnemySpawnEffectPool.Instance.SetEffect(_.transform.position);
        EnemyRepository.Instance.OnUnregister -= _ => Debug.Log($"Enemy with ID {_} just died");
    }
    #endregion
    #region MONOBEHAVIOUR
    protected virtual void OnEnable()
    {
        SubscribeOnEvents();
    }
    protected virtual void OnDisable()
    {
        UnsubscribeOnEvents();
    }
    #endregion
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
    protected virtual void RegisterUpdate()
    {
        Updater.Instance.RegisterUpdate(this, Updater.UpdateType.InitialUpdate);
    }
    protected virtual void UnregisterUpdate()
    {
        Updater.Instance.UnregisterUpdate(this, Updater.UpdateType.InitialUpdate);
    }
    #endregion
 
 */



/*public class Repository<T, TKey> where TKey : notnull
{
    public Dictionary<TKey, T> Items { get; } = new();

    public void Register(TKey id, T item) => 
        Items.Add(id, item);

    public void Unregister(TKey id) => 
        Items.Remove(id);
}*/