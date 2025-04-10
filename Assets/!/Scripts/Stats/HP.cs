using UnityEngine;
public delegate void Damage(int maxValue, int damage);
public class HP : Stats, IDamageable
{
    [SerializeField] private int _armor;
    public static event Damage OnDamage;
    private GameObject _lastAttacker;
    private int _aDamage;
    private bool dead = false;
    public override ESliderType Type { get => ESliderType.HP; protected set { } }
    public void GetDamage(int value, GameObject attacker)
    {
        _aDamage = value * (1 - _armor / 100);
        Current -= _aDamage;
        _lastAttacker = attacker;

        if (Current <= 0 && !dead) {
            dead = true;
            GenerationHandler.instance.Load("Loading", () => GenerationHandler.instance.StartCoroutine(GenerationHandler.instance.LoadScene(2)));
        }
        OnDamage?.Invoke(Max, value);
    }
    public void GetRecovery(int value)
    {
        Current += value;
    }
    public GameObject GetAttacker() => _lastAttacker;
}
