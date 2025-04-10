using System.Linq;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CheckPossibilities : MonoBehaviour
{
    [SerializeField] private float _attackRange;
    [SerializeField] private float _followRange;
    private Transform _nearestTarget;
    private AI _ai;
    private float _currentDistance;
    private float _nearestDist;

    public (bool, Transform) IsInAttackRange() => FindNearestTargetInRange(_attackRange);
    public (bool, Transform) IsInFollowRange() =>  FindNearestTargetInRange(_followRange);

    private (bool, Transform) FindNearestTargetInRange(float range)
    {   
        if(_ai.Targets.Count == 0)
        {
            Debug.Log($"{nameof(_ai.Targets.Count)} count = 0");
            return (false, null);
        }

        _nearestTarget = null;
        _nearestDist = float.MaxValue;

        foreach(var target in _ai.Targets)
        {
            if(target == null)
            {
                Debug.LogWarning($"{nameof(target)} is null in {nameof(FindNearestTargetInRange)}");
                continue;
            }

            _currentDistance = DistTo(target.position);

            if(_currentDistance < range && _currentDistance < _nearestDist)
            {
                _nearestDist = _currentDistance;
                _nearestTarget = target;
            }
        }

        return _nearestTarget != null
            ? (true, _nearestTarget)
            : (false, null);
    }
    private float DistTo(Vector3 target) => Vector3.Distance(this.transform.position, target);
    private void Awake()
    {
        if(!TryGetComponent(out _ai))
        {
            Debug.Log($"{nameof(_ai)} is null in {this.name}");
            return;
        }
    }
}