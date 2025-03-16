using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private Transform _t;
    [SerializeField] private AttackHandler _at;
    private void Start()
    {
        _at.SetAttack(_t.GetComponent<IAttack>());
    }

}



