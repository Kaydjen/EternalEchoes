using UnityEngine;

public class CheckPossibilities : MonoBehaviour
{
    [SerializeField] private LayerMask _whatIsPlayer;
    [SerializeField] private float _attackRange;
    public bool IsInAttackRange => Physics.CheckSphere(transform.position, _attackRange, _whatIsPlayer);
}