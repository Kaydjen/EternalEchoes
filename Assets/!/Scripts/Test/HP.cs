using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP : MonoBehaviour, IDamageable
{
    [SerializeField] private float _health = 100f;
    [SerializeField] private float _armor;
    [SerializeField] private float _aDamage;
    public void GetDamage(float value)
    {
        _aDamage = value * (1 - _armor / 100);
        _health -= _aDamage;
    }
}
