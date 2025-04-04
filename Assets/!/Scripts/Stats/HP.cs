using System;
using UnityEngine;

public class HP : Stats, IDamageable
{
    [SerializeField] private int _armor;
    private float _aDamage;
    public override ESliderType Type { get => ESliderType.HP; set { } }
    public override event Action<float, float> OnValueChanged;
    public void GetDamage(float value)
    {
        _aDamage = value * (1f - _armor / 100f);
        currentValue -= _aDamage;
        OnValueChanged?.Invoke(currentValue, maxValue);
    }
}
