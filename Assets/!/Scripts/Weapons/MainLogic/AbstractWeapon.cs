using UnityEngine;

public abstract class AbstractWeapon : MonoBehaviour
{
    public WeaponScriptableAbstract Data;
    public abstract void Attack();
}
