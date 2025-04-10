using UnityEngine;

public class BuyNewWeapon : MonoBehaviour
{
    public void BuySlash(ESlashAttack tupe)
    {
        var weapon = PlayerCore.Instance.transform.GetComponentInChildren<SlashAttackInstantiater>();
        if(weapon == null)
        {
            Debug.Log("weapon");
            return;
        }
        weapon.ChangeWeapon(tupe);
    }
    public void BuyProge(EProjectile tupe)
    {
        var weapon = PlayerCore.Instance.transform.GetComponentInChildren<ProjectileInstantiater>();
        if(weapon == null)
        {
            Debug.Log("weapon");
            return;
        }
        weapon.ChangeWeapon(tupe);
    }
}