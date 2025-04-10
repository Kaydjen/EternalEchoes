using UnityEngine;
using UnityEngine.AI;

public class RuleAnimations : MonoBehaviour, IRule
{
    private NavMeshAgent _agent;
    private Animator _animator;

    public bool CanExecute()
    {
        return true;
    }

    public void Execute()
    {
        if(_agent.velocity.magnitude > 0f && _agent.hasPath)
        {
            _animator.SetBool("IsFollowing", true);
        }
        else
        {
            _animator.SetBool("IsFollowing", false);
        }
    }

    private void Awake()
    {
        if(!TryGetComponent(out _animator))
        {
            Debug.Log($"{nameof(_animator)} is null in {this.name}");
            return;
        }
        if(!TryGetComponent(out _agent))
        {
            Debug.Log($"{nameof(_agent)} is null in {this.name}");
            return;
        }
    }
}
