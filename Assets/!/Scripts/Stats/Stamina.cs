using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Stamina : MonoBehaviour
{
    [SerializeField] private float _maxStamina;
    [SerializeField] private float _currentStamina;
    private float _lastStamina;

    [SerializeField] private float _staminaDelay;
    [SerializeField] private float _regenSpeed;
    private float _lastTimeShot;
    private bool hasShot = false;

    private Coroutine _staminaCoroutine;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) SubtractStamina(1f);

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
    public float MaxStamina
    {
        get { return _maxStamina; }
        set { _maxStamina = value; }
    }

    public void SubtractStamina(float value)
    {
        if (_staminaCoroutine != null)
        {
            StopCoroutine(_staminaCoroutine);
            _staminaCoroutine = null;
            Debug.Log("Зупинив корутину");
        }
        _lastTimeShot = Time.time + _staminaDelay;
        _currentStamina -= value;
        hasShot = true;
        Debug.Log("ya strelnyv");
    }
    private void AddSmoothly()
    {
        
    }

    private IEnumerator StaminaAdd()
    {
        while (_currentStamina < _maxStamina)
        {
            _currentStamina += 1f;
            yield return new WaitForSeconds(_regenSpeed);
        }
        _currentStamina = _maxStamina;
        _staminaCoroutine = null;
        Debug.Log("Регенерація завершена");
    }
    public void AddStamina()
    {

    }
}
