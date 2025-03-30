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
        if (agent != null) agent.enabled = false;
        if (enemyRb != null) enemyRb.isKinematic = true;

        yield return new WaitForSeconds(duration);

        if (agent != null) agent.enabled = true;
        if (enemyRb != null) enemyRb.isKinematic = false;
    }
}