using UnityEngine;

class Gun : MonoBehaviour, IAttack 
{
    public void Attack()
    {
        CW.I.Print("ATTACK!!", 1);
    }
}
