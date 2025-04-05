using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class TornadoPull : MonoBehaviour
{
    public static TornadoPull Instance;

    [SerializeField] private float pullSpeed = 5f;
    [SerializeField] private float damageInterval = 0.5f;
    private void Awake()
    {
        Instance = this;
    }

    public void Pull(NavMeshAgent agent, GameObject tornado, int damage)
    {
        StartCoroutine(PullCoroutine(agent, tornado, damage));
    }

    private IEnumerator PullCoroutine(NavMeshAgent agent, GameObject tornado, int damage)
    {
        if (agent == null) yield break;

        agent.enabled = false;

        while (tornado.activeSelf)
        {
            agent.transform.position = Vector3.MoveTowards(agent.transform.position, tornado.transform.position, pullSpeed * Time.deltaTime);
            StartCoroutine(ApplyDamage(agent, damage));
            yield return null;
        }

        if (agent != null) agent.enabled = true;
    }
    private IEnumerator ApplyDamage(NavMeshAgent agent, int damage)
    {
        yield return new WaitForSeconds(damageInterval);
        IDamageable damageable = agent.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.GetDamage(damage);
        }
    }
}