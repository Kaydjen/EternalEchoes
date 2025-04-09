using System.Linq;
using UnityEngine;

public class CheckPossibilities : MonoBehaviour
{
    [SerializeField] private AI _ai;
    [SerializeField] private float _attackRange;
    [SerializeField] private float _followRange;
    private Transform _nearestTarget;
    private float _dist;
    private float _nearestDist;
    public bool IsInAttackRange()
    {
        if(_ai.Targets.Count == 0) return false;
        _nearestTarget = null;
        foreach(var target in _ai.Targets)
        {
            if(target == null)
            {
                Debug.Log($"{nameof(target)} is null in {nameof(CheckPossibilities)}");
                continue;
            }
            _dist = DistTo(_ai.Targets.FirstOrDefault().position);
            if(_dist < _attackRange)
            {
                if(_dist < _nearestDist)
                {
                    _nearestTarget = target;
                }
            }
        }
        return true;
    }
    public bool IsInFollowRange()
    {
        if(_ai.Targets.Count == 0) return false;
        foreach(var target in _ai.Targets)
        {
            if(target == null)
            {
                Debug.Log($"{nameof(target)} is null in {nameof(CheckPossibilities)}");
                continue;
            }
            if(DistTo(_ai.Targets.FirstOrDefault().position) < _followRange) return true;
            else return false;
        }
        return false;
    }
    private float DistTo(Vector3 target)
    {
        return Vector3.Distance(this.transform.position, target);
    }
    public bool CanFollow()
    {
        return false;
    }
}