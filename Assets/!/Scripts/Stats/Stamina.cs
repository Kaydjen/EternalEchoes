using System;
using System.Collections;
using UnityEngine;

public class Stamina : Stats
{
    [SerializeField] private float _staminaDelay;
    [SerializeField] private float _regenSpeed;
    private Coroutine _staminaCoroutine;
    private float _lastTimeShot;
    private bool hasShot = true;

    public override ESliderType Type { get => ESliderType.Stamina; set { } }
    public override event Action<float, float> OnValueChanged;

    private void Update()
    {
        if (_lastTimeShot < Time.time && hasShot)
        {
            hasShot = false;
            if (_staminaCoroutine == null)
            {
                _staminaCoroutine = StartCoroutine(StaminaAdd());
            }
        }
    }
    public bool SubtractStamina(float value)
    {
        if (currentValue - value < 0) return false;
        if (_staminaCoroutine != null)
        {
            StopCoroutine(_staminaCoroutine);
            _staminaCoroutine = null;
        }
        _lastTimeShot = Time.time + _staminaDelay;
        currentValue -= value;
        OnValueChanged?.Invoke(currentValue, maxValue);
        hasShot = true;
        return true;
    }
    private IEnumerator StaminaAdd()
    {
        while (currentValue < maxValue)
        {
            currentValue += 1f;
            OnValueChanged?.Invoke(currentValue, maxValue);
            yield return new WaitForSeconds(_regenSpeed);
        }
        currentValue = maxValue;
        _staminaCoroutine = null;
    }
}


/*
 
    [SerializeField] private float _staminaDelay;
    [SerializeField] private float _regenSpeed;
    private Coroutine _staminaCoroutine;
    private float _lastTimeShot;
    private bool hasShot = true;

    public override ESliderType Type { get => ESliderType.Stamina; set { } }
    public override event Action<float, float> OnValueChanged;

    private void Update()
    {
        if (_lastTimeShot < Time.time && hasShot)
        {
            hasShot = false;
            if (_staminaCoroutine == null)
            {
                _staminaCoroutine = StartCoroutine(StaminaAdd());
            }
        }
    }
    public bool SubtractStamina(float value)
    {
        if(currentValue - value < 0) return false;
        if (_staminaCoroutine != null)
        {
            StopCoroutine(_staminaCoroutine);
            _staminaCoroutine = null;
        }
        _lastTimeShot = Time.time + _staminaDelay;
        currentValue -= value;
        OnValueChanged?.Invoke(currentValue, maxValue);
        hasShot = true;
        return true;
    }
    private IEnumerator StaminaAdd()
    {
        while (currentValue < maxValue)
        {
            currentValue += 1f;
            OnValueChanged?.Invoke(currentValue, maxValue);
            yield return new WaitForSeconds(_regenSpeed);
        }
        currentValue = maxValue;
        _staminaCoroutine = null;
    }
 
 */

/*
 
 
     [SerializeField] private float _staminaDelay;
    [SerializeField] private float _regenSpeed;
    private Coroutine _staminaCoroutine;
    private Coroutine _delayedRegenerationCoroutine;
    public override ESliderType Type { get => ESliderType.Stamina; set { } }
    public override event Action<float, float> OnValueChanged;

    private void Start()
    {
        _delayedRegenerationCoroutine = StartCoroutine(StartDelayedRegeneration());
            StopCoroutine(_delayedRegenerationCoroutine);
        _staminaCoroutine = StartCoroutine(StaminaAdd());
            StopCoroutine(_staminaCoroutine);

        if (SubtractStamina(1)) { }
    }
    public bool SubtractStamina(float value)
    {
        if (currentValue - value < 0) return false;

        StopCoroutine(_staminaCoroutine);      

        currentValue -= value;
        OnValueChanged?.Invoke(currentValue, maxValue);

        StartCoroutine(StartDelayedRegeneration());
        return true;
    }

    private IEnumerator StartDelayedRegeneration()
    {
        yield return new WaitForSeconds(_staminaDelay);
        _staminaCoroutine = StartCoroutine(StaminaAdd());
    }

    private IEnumerator StaminaAdd()
    {
        while (currentValue < maxValue)
        {
            currentValue += 1f;
            OnValueChanged?.Invoke(currentValue, maxValue);
            yield return new WaitForSeconds(_regenSpeed);
        }
        currentValue = maxValue;
    }
 
 */