using UnityEngine;

public class Gun : AbstractWeapon
{
    private GunScriptable _data; 

    private void Start() => _data = Data as GunScriptable;
    public override void Attack()
    {
        Debug.Log("BAM!!! - " + _data.Damage);
        Debug.Log("Tadada!!! - " + _data.CloudDamage);
    }
}
