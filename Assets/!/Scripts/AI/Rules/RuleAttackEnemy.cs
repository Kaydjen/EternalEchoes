using System.Collections;
using Optimization;
using UnityEngine;
using UnityEngine.AI;

public class RuleAttackEnemy : MonoBehaviour, IRule
{
    [SerializeField] private AnimationClip _attackAnimation;
    private CheckPossibilities _possibilities;
    private Animator _animator;
    private Transform _target;
    private AI _ai;
    private NavMeshAgent _agent;
    private Coroutine _coroutine;
    public bool CanExecute()
    {
        var statement = _possibilities.IsInAttackRange();
        var statement2 = _possibilities.IsInFollowRange();

        if(statement.Item1 && statement2.Item1)
        {
            _target = statement.Item2;
            return true;
        }
        else
        {
            return false;
        }
    }
    public void Execute()
    {
        if(_agent.isStopped) return;
        _coroutine = StartCoroutine(Attack());
    }
    private IEnumerator Attack()
    {
        _agent.isStopped = true;
        _animator.SetTrigger("DidAttack");
        this.transform.LookAt(_target);
        //_attackAnimation.Play();
        yield return new WaitForSeconds(_attackAnimation.length);
        _agent.isStopped = false;
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
