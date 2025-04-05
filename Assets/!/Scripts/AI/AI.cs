using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AI : MonoBehaviour
{
    [SerializeField] protected NavMeshAgent _agent;
    protected virtual void SetDestionaiton(Vector3 coordinates)
    {
        _agent.destination = coordinates;
    }
    protected virtual void Awake()
    {
        EnemyRepository.Instance.Register(this.gameObject, this.GetInstanceID());
    }
    protected void OnDestroy()
    {
        EnemyRepository.Instance.Unregister(this.GetInstanceID(), this.gameObject);
    }
    protected virtual void OnEnable()
    {
        _agent.enabled = true;
    }
    protected virtual void OnDisable()
    {
        _agent.enabled = false;
    }
}
