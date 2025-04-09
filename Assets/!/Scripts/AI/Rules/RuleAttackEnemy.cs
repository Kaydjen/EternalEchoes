using Optimization;
using UnityEngine;

public class RuleAttackEnemy : MonoBehaviour, IRule
{
    [SerializeField] private CheckPossibilities _possibilities;
    [SerializeField] private AI _ai;
    
    public bool CanExecute()
    {
        return _possibilities.IsInAttackRange();
    }
    public void Execute()
    {
        Debug.Log("Im attacking u");
    }
}
public class RuleFollowEnemy : MonoBehaviour, IRule
{
    [SerializeField] private CheckPossibilities _possibilities;
    [SerializeField] private AI _ai;


    public bool CanExecute()
    {
        return _possibilities.IsInFollowRange();
    }

    public void Execute()
    {
        _ai.SetDestionaiton();
    }
}