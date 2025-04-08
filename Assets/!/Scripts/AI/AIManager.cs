using System.Collections;
using UnityEngine;

public class AIManager : MonoBehaviour
{

    #region PRIVATE 
    private void SubscribeOnEvents()
    {
        EnemyRepository.OnRegister += obj => StartCoroutine(SpawnEffect(obj.gameObject));
        EnemyRepository.OnUnregister += id => Debug.Log($"Enemy with ID {id} just died");
        EnemyRepository.OnDeath += SoulInstallManager;
    }
    private void UnsubscribeOnEvents()
    { 
        EnemyRepository.OnRegister -= obj => StartCoroutine(SpawnEffect(obj.gameObject));
        EnemyRepository.OnUnregister -= id => Debug.Log($"Enemy with ID {id} just died");
        EnemyRepository.OnDeath -= SoulInstallManager;
    }
    private IEnumerator SpawnEffect(GameObject obj)
    {
        EnemySpawnEffectPool.Instance.SetEffect(obj.transform.position);
        yield return new WaitForSeconds(2f);
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




