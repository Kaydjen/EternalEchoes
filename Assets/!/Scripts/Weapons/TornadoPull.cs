using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class TornadoPull : MonoBehaviour
{
    public static TornadoPull Instance;
    private void Awake()
    {
        Instance = this;
    }
    public void Pull(NavMeshAgent agent, float duration)
    {
        StartCoroutine(PullCoroutine(agent, duration));
    }
    private IEnumerator PullCoroutine(NavMeshAgent agent, float duration)
    {
        if (agent == null) yield break;

        agent.enabled = false;

        yield return new WaitForSeconds(duration);

        agent.enabled = true;
    }
}