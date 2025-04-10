using UnityEngine;

public class BuyNewWeapon : MonoBehaviour
{
    public void BuyElectricity()
    {
        var weapon = PlayerCore.Instance.transform.GetComponentInChildren<SlashAttackInstantiater>();
        if(weapon == null)
        {
            Debug.Log("weapon");
            return;
        }
        weapon.ChangeWeapon(ESlashAttack.Electricity);
    }
    public void BuyIce()
    {
        var weapon = PlayerCore.Instance.transform.GetComponentInChildren<SlashAttackInstantiater>();
        if(weapon == null)
        {
            Debug.Log("weapon");
            return;
        }
        weapon.ChangeWeapon(ESlashAttack.Ice);
    }
    public void BuyFireball()
    {
        var weapon = PlayerCore.Instance.transform.GetComponentInChildren<ProjectileInstantiater>();
        if(weapon == null)
        {
            Debug.Log("weapon");
            return;
        }
        weapon.ChangeWeapon(EProjectile.Fireball);
    }
    public void BuyIcicle()
    {
        var weapon = PlayerCore.Instance.transform.GetComponentInChildren<ProjectileInstantiater>();
        if(weapon == null)
        {
            Debug.Log("weapon");
            return;
        }
        weapon.ChangeWeapon(EProjectile.Icicle);
    }
    public void BuyTornado()
    {
        var weapon = PlayerCore.Instance.transform.GetComponentInChildren<ProjectileInstantiater>();
        if(weapon == null)
        {
            Debug.Log("weapon");
            return;
        }
        weapon.ChangeWeapon(EProjectile.Tornado);
    }
}