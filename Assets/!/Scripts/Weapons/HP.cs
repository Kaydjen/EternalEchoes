using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP : MonoBehaviour, IDamageable
{
    [SerializeField] private int _health = 100;
    [SerializeField] private int _armor;
    private int _aDamage;
    public void GetDamage(int value)
    {
        _aDamage = value * (1 - _armor / 100);
        _health -= _aDamage;
    }
}
