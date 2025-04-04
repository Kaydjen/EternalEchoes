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
