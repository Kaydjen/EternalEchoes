using UnityEngine;

public abstract class AbstractWeapon<T> : MonoBehaviour where T : WeaponScriptableAbstract
{
    public T Data; 
    public abstract void Attack();
}
