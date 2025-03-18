using UnityEngine;

[RequireComponent(typeof(AttackHandlerOneAttack))]
public class GateOfBabylon : MonoBehaviour, IAttack
{
    public void Attack()
    {
        // GetComponent<AimPlacer>().Particle.position
        CW.I.Print($"Attack {GetComponent<AimPlacer>().GetAimTransform().position}", 10);
    }
}