using UnityEngine;

public class HP : Stats, IDamageable
{
    [SerializeField] private int _armor;
    private GameObject _lastAttacker;
    private int _aDamage;
    public override ESliderType Type { get => ESliderType.HP; protected set { } }
    public void GetDamage(int value, GameObject attacker)
    {
        _aDamage = value * (1 - _armor / 100);
        Debug.Log("_aDamage class _hp = " + _aDamage);
        Current -= _aDamage;
        _lastAttacker = attacker;
    }
    public void GetRecovery(int value)
    {
        Current += value;
    }
    public GameObject GetAttacker() => _lastAttacker;
}
