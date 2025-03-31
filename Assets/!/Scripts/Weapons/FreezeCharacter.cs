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
    public void Freeze(NavMeshAgent agent, float duration)
    {
        StartCoroutine(FreezeCoroutine(agent, duration));
    }
    private IEnumerator FreezeCoroutine(NavMeshAgent agent, float duration)
    {
        if (agent == null) yield break;

        agent.enabled = false;

        yield return new WaitForSeconds(duration);

        agent.enabled = true;
    }
}
