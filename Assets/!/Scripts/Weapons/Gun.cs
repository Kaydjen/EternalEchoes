using UnityEngine;

public class Gun : AbstractWeapon<GunScriptable>
{
    public override void Attack()
    {
        Debug.Log("BAM!!! - " + Data.Damage);
        Debug.Log("Tadada!!! - " + Data.CloudDamage);
    }
}
