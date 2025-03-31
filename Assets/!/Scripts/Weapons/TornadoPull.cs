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

    public void Pull(NavMeshAgent agent)
    {
        StartCoroutine(PullCoroutine(agent));
    }

    private IEnumerator PullCoroutine(NavMeshAgent agent)
    {
        if (agent == null) yield break;

        agent.enabled = false;

        while (gameObject.activeSelf)
        {
            if (agent != null)
            {
                agent.transform.position = Vector3.MoveTowards(agent.transform.position, transform.position, pullSpeed * Time.deltaTime);
            }
            yield return null;
        }

        if (agent != null) agent.enabled = true;
    }
}