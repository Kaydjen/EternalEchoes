using UnityEngine;

class A_Gun : MonoBehaviour, IAttack 
{
    public void Attack()
    {
        CW.I.Print("ATTACK!!", 1);
    }
}
