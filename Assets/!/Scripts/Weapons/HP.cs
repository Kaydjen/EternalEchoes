using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP : MonoBehaviour, IDamageable, ISliderValue
{
    [SerializeField] private float _maxHealth = 100;
    [SerializeField] private float _currentHealth = 100;
    [SerializeField] private int _armor;
    private float _aDamage;

    public float Max
    {
        get { return _maxHealth; }
        set
        {
            _maxHealth = value;
            OnValueChanged?.Invoke(_currentHealth, _maxHealth);
        }
    }

    public float Current
    {
        get { return _currentHealth; }
        set
        {
            _currentHealth = Mathf.Clamp(value, 0f, _maxHealth);
        }
    }

    public event Action<float, float> OnValueChanged;

    public void GetDamage(float value)
    {
        _aDamage = value * (1f - _armor / 100f);
        _currentHealth -= _aDamage;
        if(_currentHealth<=0)
        {
            //Logic for death
            Destroy(gameObject);
        }
        OnValueChanged?.Invoke(_currentHealth, _maxHealth);
    }
}
