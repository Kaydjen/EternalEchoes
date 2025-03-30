using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class FreezeCharacter : MonoBehaviour
{
    public static FreezeCharacter Instance;
    private void Awake()
    {
        Instance = this;
    }
    public void Freeze(NavMeshAgent agent, Rigidbody enemyRb, float duration)
    {
        StartCoroutine(FreezeCoroutine(agent, enemyRb, duration));
    }
    private IEnumerator FreezeCoroutine(NavMeshAgent agent, Rigidbody enemyRb, float duration)
    {
        if (agent == null) yield break;
        if (enemyRb == null) yield break;

        agent.enabled = false;
        enemyRb.isKinematic = true;

        yield return new WaitForSeconds(duration);

        agent.enabled = true;
        enemyRb.isKinematic = false;
    }
}