using UnityEngine;

public class RuleFollowEnemy : MonoBehaviour, IRule
{
    private CheckPossibilities _possibilities;
    private Animator _animator;
    private Transform _target;
    private AI _ai;
    public bool CanExecute()
    {
        var statement = _possibilities.IsInAttackRange();
        var statement2 = _possibilities.IsInFollowRange();
        if(!statement.Item1 && statement2.Item1)
        {
            _target = statement2.Item2;
            return true;
        }
        else
        {
            _animator.SetBool("IsFollowing", false);
            return false;
        }
    }
    public void Execute()
    {
        _ai.SetDestination(_target);
        _animator.SetBool("IsFollowing", true);
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
    }
}