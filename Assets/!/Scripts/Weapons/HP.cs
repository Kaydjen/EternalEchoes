using System;
using UnityEngine;

public class HP : MonoBehaviour, IDamageable, ISliderValue
{
    [SerializeField] private float _maxHealth = 100;
    [SerializeField] private float _currentHealth = 100;
    [SerializeField] private int _armor;
    private float _aDamage;

    public ESliderType Type { get => ESliderType.HP; set { } }
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

    private void Start()
    {
        OnValueChanged?.Invoke(_currentHealth, _maxHealth);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            GetDamage(30f);
        }
    }

    public void GetDamage(float value)
    {
        _aDamage = value * (1f - _armor / 100f);
        _currentHealth -= _aDamage;
        OnValueChanged?.Invoke(_currentHealth, _maxHealth);
    }
}
