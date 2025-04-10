using System.Collections;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    #region PRIVATE 
    private void SubscribeOnEvents()
    {
        AIRepository.OnInstantiate += ai => Minimap.MinimapHandler.Add(new Minimap.MoveableMinimapObject(ai.transform, 0, Color.white));
        AIRepository.OnInstantiate += ai => ai.gameObject.SetActive(true);
        AIRepository.OnRegister += ai => StartCoroutine(SpawnEffect(ai));
        AIRepository.OnUnregister += ai => Debug.Log($"Enemy with ID {ai.gameObject.GetInstanceID()} just died");
        AIRepository.OnDeath += SoulInstallManager;
    }
    private void UnsubscribeOnEvents()
    { 
        AIRepository.OnRegister -= ai => StartCoroutine(SpawnEffect(ai));
        AIRepository.OnUnregister -= ai => Debug.Log($"Enemy with ID {ai.gameObject.GetInstanceID()} just died");
        AIRepository.OnDeath -= SoulInstallManager;
    }
    private IEnumerator SpawnEffect(AI ai)
    {
        yield return null;
        EnemySpawnEffectPool.Instance.SetEffect(ai.transform.position);
        yield return new WaitForSeconds(4.3f);
        AIRepository.InvokeOnInstantiate(ai);
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




