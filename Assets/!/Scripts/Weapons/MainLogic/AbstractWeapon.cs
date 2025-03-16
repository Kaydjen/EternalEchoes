using UnityEngine;

public abstract class AbstractWeapon: MonoBehaviour
{
    public WeaponScriptableAbstract Data; 
    public abstract void Attack();
    public abstract void EnterAimingMode();
    public abstract void ExitAimingMode();
    protected abstract void Start();
}
