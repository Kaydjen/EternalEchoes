using UnityEngine;

[RequireComponent(typeof(AttackHandler))] 
[RequireComponent(typeof(AimHandler))]
public class GateOfBabylon : MonoBehaviour, IAttack, IAim
{
    public GameObject AimParticle;
    public void Attack()
    {
        CW.I.Print("Attack", 10);
    }

    public void StartAim()
    {
        CW.I.Print("StartAim", 10);
    }

    public void StopAim()
    {
        CW.I.Print("StopAim", 10);
    }
}