using UnityEngine;

public class RuleFollowEnemy : MonoBehaviour, IRule
{
    [SerializeField] private CheckPossibilities _possibilities;
    [SerializeField] private AI _ai;
    private Transform _target;
    public bool CanExecute()
    {
        var elements = _possibilities.IsInFollowRange();
        _target = elements.Item2;
        return elements.Item1;
    }
    public void Execute()
    {
        Debug.Log("I follow you");
        _ai.SetDestination(_target);
    }
}