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

        ESoulType soulType = soul.GetCurrentSoulType();

        if (DSouls.List.TryGetValue(soulType, out Pool<Transform> pool))
        {
            Transform installedSoul = pool.Get();
            installedSoul.position = ai.transform.position;

            if (ai.transform.TryGetComponent(out EnemyHP enemyHP) 
                && enemyHP.GetAttacker() is { } attacker
                && installedSoul.TryGetComponent(out HomingSoul homingSoul))
            {
                homingSoul.SetDestination(attacker.transform);
                homingSoul.SetSoulValue(soul.GetSoulValue());
            }
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

