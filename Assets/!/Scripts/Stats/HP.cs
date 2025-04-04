using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HP : Stats, IDamageable
{
    [SerializeField] private int _armor;
    private int _aDamage;
    public override ESliderType Type { get => ESliderType.HP; protected set { } }
    public void GetDamage(int value)
    {
        _aDamage = value * (1 - _armor / 100);
        Debug.Log("_aDamage class HP = " + _aDamage);
        Current -= _aDamage;
    }
    public void GetRecovery(int value)
    {
        Current += value;
    }
}




public class HPRecovery : Stats, IGameplayModeSwitcher
{
    public override ESliderType Type { get => ESliderType.Recovery; protected set { } }
    public override event Action<int, int> OnValueChanged;

    [Header("One point = one player hp = enemy soul's cost")]
    [SerializeField] private List<int> _levels = new(); 
    private int _value;
    public void Recovery()
    {
        if(TryGetComponent(out HP hp))
        {
            hp.GetRecovery(_value);
            
        }
    }
    public void Replenish(int value)
    {
        if(_value + value > _levels[currentValue])
        {
            int remainder = currentValue + value - _levels[currentValue];
            Current++;
            _value = remainder;
        }
        else
        {
            _value += value;
        }
    }
    public void ForAIMode()
    {
        maxValue = _levels.Count;
    }
    public void ForManualMode()
    {

    }
}
