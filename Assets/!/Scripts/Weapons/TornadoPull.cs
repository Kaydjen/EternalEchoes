using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class TornadoPull : MonoBehaviour
{
    public static TornadoPull Instance;

    [SerializeField] private float pullSpeed = 5f;
    private void Awake()
    {
        Instance = this;
    }

    public void Pull(NavMeshAgent agent, GameObject tornado)
    {
        StartCoroutine(PullCoroutine(agent, tornado));
    }

    private IEnumerator PullCoroutine(NavMeshAgent agent, GameObject tornado)
    {
        if (agent == null) yield break;

        agent.enabled = false;

        while (tornado.activeSelf)
        {
            agent.transform.position = Vector3.MoveTowards(agent.transform.position, tornado.transform.position, pullSpeed * Time.deltaTime);
            yield return null;
        }

        if (agent != null) agent.enabled = true;
    }
}