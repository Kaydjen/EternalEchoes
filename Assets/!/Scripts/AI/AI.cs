using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AI : MonoBehaviour
{
    protected NavMeshAgent _agent;
    protected virtual void SetDestionaiton(Vector3 coordinates)
    {
        _agent.destination = coordinates;
    }
    protected virtual void Start()
    {
        if(!TryGetComponent(out _agent))
            Debug.Log($"In AI {this.gameObject.name} in script {this.name} the NavMeshAgent component can't be getted");
    }
    
}
