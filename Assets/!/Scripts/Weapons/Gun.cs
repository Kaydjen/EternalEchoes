using UnityEngine;

public class Gun : AbstractWeapon
{
    public override void Attack()
    {
        Debug.Log("BAM!!! -" + Data.Damage);
        Debug.Log("Tadada!!! -" + (Data as GunScriptable).CloudDamage);
    }
}
