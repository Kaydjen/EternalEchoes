using System.Collections;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    #region PRIVATE 
    private void SubscribeOnEvents()
    {
        EnemyRepository.Instance.OnRegister += obj => StartCoroutine(SpawnEffect(obj.gameObject));
        EnemyRepository.Instance.OnUnregister += id => Debug.Log($"Enemy with ID {id} just died");
        EnemyRepository.Instance.OnDeath += SoulInstallManager;
    }
    private void UnsubscribeOnEvents()
    { 
        EnemyRepository.Instance.OnRegister -= obj => StartCoroutine(SpawnEffect(obj.gameObject));
        EnemyRepository.Instance.OnUnregister -= id => Debug.Log($"Enemy with ID {id} just died");
        EnemyRepository.Instance.OnDeath -= SoulInstallManager;
    }
    private IEnumerator SpawnEffect(GameObject obj)
    {
        obj.SetActive(false);
        EnemySpawnEffectPool.Instance.SetEffect(obj.transform.position);
        yield return new WaitForSeconds(3f);
        obj.SetActive(true);
    }

    private void SoulInstallManager(AI ai)
    {
       // if (ai.Type == EAIType.Character) return;
        if(!ai.TryGetComponent(out SoulsLevelsHandler soul)) return;

        Transform installedSoul;
        switch (soul.GetCurrentSoulType())
        {
            case ESoulType.Newborn: 
                installedSoul = SoulPoolOne.Instance.Get();
                installedSoul.position = ai.transform.position;
                installedSoul.GetComponent<HomingSoul>().SetDestination(ai.transform.GetComponent<EnemyHP>().GetAttacker().transform);
                break;
            case ESoulType.Growing:
                installedSoul = SoulPoolTwo.Instance.Get();
                installedSoul.position = ai.transform.position;
                installedSoul.GetComponent<HomingSoul>().SetDestination(ai.transform.GetComponent<EnemyHP>().GetAttacker().transform); 
                break;
            case ESoulType.Fading:
                installedSoul = SoulPoolThree.Instance.Get();
                installedSoul.position = ai.transform.position;
                installedSoul.GetComponent<HomingSoul>().SetDestination(ai.transform.GetComponent<EnemyHP>().GetAttacker().transform); 
                break;
            case ESoulType.Lost:
                installedSoul = SoulPoolFour.Instance.Get();
                installedSoul.position = ai.transform.position;
                installedSoul.GetComponent<HomingSoul>().SetDestination(ai.transform.GetComponent<EnemyHP>().GetAttacker().transform); 
                break;
        }
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