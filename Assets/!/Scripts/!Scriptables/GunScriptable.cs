using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName = "Weapons/WeaponScriptable", order = 2)]
public class GunScriptable : WeaponScriptableAbstract
{
    public int _totalAmmo;
    public int _currentAmmo;
    public bool _isMagEmpty = false;
    public float _range;
    public float _fireRate;
    public float _reloadTime;
}

