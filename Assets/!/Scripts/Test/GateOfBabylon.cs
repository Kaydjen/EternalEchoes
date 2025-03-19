using UnityEngine;

[RequireComponent(typeof(AttackHandlerOneAttack))]
public class GateOfBabylon : MonoBehaviour, IAttack
{
    [SerializeField] private GameObject _particle;
    [SerializeField] private float _height = 20f;
    public void Attack()
    {
        // GetComponent<AimPlacer>().Particle.position
        CW.I.Print($"Attack {GetComponent<AimPlacer>().GetAimTransform().position}", 10);
        Vector3 pos = GetComponent<AimPlacer>().GetAimTransform().position;
        pos.y += _height;
        Instantiate(_particle, pos, Quaternion.identity);
    }
}