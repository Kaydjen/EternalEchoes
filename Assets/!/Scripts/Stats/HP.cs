using System;
using System.Collections.Generic;
using UnityEngine;

public class HP : Stats, IDamageable
{
    [SerializeField] private int _armor;
    private float _aDamage;
    public override ESliderType Type { get => ESliderType.HP; protected set { } }
    public void GetDamage(float value)
    {
        _aDamage = value * (1f - _armor / 100f);
        Current -= _aDamage;
    }
}




public class HPRecovery : Stats, IGameplayModeSwitcher
{
    public override ESliderType Type { get => ESliderType.Recovery; protected set { } }
    public override event Action<float, float> OnValueChanged;

    [SerializeField] private List<float> _levels = new();

    public override float Max { get => _levels.Count; protected set { } }
    public override float Current { get => base.Current; protected set => base.Current = Mathf.Floor(value); }
    public void Recovery()
    {

    }
    public void Replenish(float value)
    {

    }
    public void ForAIMode()
    {

    }
    public void ForManualMode()
    {

    }
}
