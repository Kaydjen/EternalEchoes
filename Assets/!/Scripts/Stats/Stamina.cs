using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Stamina : MonoBehaviour, ISliderValue
{
    [SerializeField] private float _maxStamina;
    [SerializeField] private float _currentStamina;

    [SerializeField] private float _staminaDelay;
    [SerializeField] private float _regenSpeed;
    private float _lastTimeShot;
    private bool hasShot = false;

    private Coroutine _staminaCoroutine;

    public event Action<float, float> OnValueChanged;

    public float Max
    {
        get { return _maxStamina; }
        set 
        { 
            _maxStamina = value;
            OnValueChanged?.Invoke(_currentStamina, _maxStamina);
        }
    }

    public float Current
    {
        get { return _currentStamina;}
        set 
        {
           _currentStamina = Mathf.Clamp(value, 0f , _maxStamina);
        }
    } 

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) SubtractStamina(30f);

        if (_lastTimeShot < Time.time && hasShot)
        {
            hasShot = false;
            if (_staminaCoroutine == null)
            {
                _staminaCoroutine = StartCoroutine(StaminaAdd());
                Debug.Log("Почалась регенерація");
            }
        }
    }

    public bool SubtractStamina(float value)
    {
        if(_currentStamina - value < 0) return false;
        if (_staminaCoroutine != null)
        {
            StopCoroutine(_staminaCoroutine);
            _staminaCoroutine = null;
            Debug.Log("Зупинив корутину");
        }
        _lastTimeShot = Time.time + _staminaDelay;
        _currentStamina -= value;
        OnValueChanged?.Invoke(_currentStamina, _maxStamina);
        hasShot = true;
        Debug.Log("ya strelnyv");
        return true;
    }

    private IEnumerator StaminaAdd()
    {
        while (_currentStamina < _maxStamina)
        {
            _currentStamina += 1f;
            OnValueChanged?.Invoke(_currentStamina, _maxStamina);
            yield return new WaitForSeconds(_regenSpeed);
        }
        _currentStamina = _maxStamina;
        _staminaCoroutine = null;
        Debug.Log("Регенерація завершена");
    }
}
