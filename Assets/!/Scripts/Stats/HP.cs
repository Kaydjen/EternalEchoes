using UnityEngine;
public delegate void Damage(int hp, int damage);
public class HP : Stats, IDamageable
{
    [SerializeField] private int _armor;
    public static event Damage OnDamage;
    private GameObject _lastAttacker;
    private int _aDamage;
    public override ESliderType Type { get => ESliderType.HP; protected set { } }
    public void GetDamage(int value, GameObject attacker)
    {
        _aDamage = value * (1 - _armor / 100);
        Debug.Log("_aDamage class _hp = " + _aDamage);
        Current -= _aDamage;
        _lastAttacker = attacker;
        OnDamage?.Invoke(Current, value);
    }
    public void GetRecovery(int value)
    {
        Current += value;
    }
    public GameObject GetAttacker() => _lastAttacker;
}
