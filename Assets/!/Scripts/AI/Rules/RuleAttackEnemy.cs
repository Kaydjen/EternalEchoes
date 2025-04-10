using System.Collections;
using Optimization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class RuleAttackEnemy : MonoBehaviour, IRule
{
    [SerializeField] private AnimationClip _attackAnimation;
    private CheckPossibilities _possibilities;
    private bool _isAttacking = false;  // Дополнительный флаг для надежности
    private Animator _animator;
    private Transform _target;
    private AI _ai;
    private NavMeshAgent _agent;
    public bool CanExecute()
    {
        var statement = _possibilities.IsInAttackRange();
        var statement2 = _possibilities.IsInFollowRange();

        if(statement.Item1 && statement2.Item1)
        {
            _target = statement.Item2;
            Debug.Log("Attack true");
            return true;
        }
        else
        {
            return false;
        }
    }
    public void Execute()
    {
        if(_agent.isStopped || _isAttacking) return;
        Debug.Log("Attack started");
        _isAttacking = true;
        _agent.isStopped = true;
        _animator.SetTrigger("DidAttack");
        transform.LookAt(_target);
        Invoke(nameof(StopAttack),_attackAnimation.length);
    }
    private void StopAttack()
    {
        _agent.isStopped = false;
        _isAttacking = false;
        Debug.Log("Attack finished");
    }
    private void Awake()
    {
        if(!TryGetComponent(out _ai))
        {
            Debug.Log($"{nameof(_ai)} is null in {this.name}");
            return;
        }
        if(!TryGetComponent(out _possibilities))
        {
            Debug.Log($"{nameof(_possibilities)} is null in {this.name}");
            return;
        }
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
