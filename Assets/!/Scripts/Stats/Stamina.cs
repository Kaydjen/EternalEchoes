using System.Collections;
using UnityEngine;

public class Stamina : Stats, IGameplayModeSwitcher, IUpdate
{
    [SerializeField] private float _staminaDelay;
    [SerializeField] private float _regenSpeed;
    private Coroutine _staminaCoroutine;
    private float _lastTimeShot;
    private bool hasShot = true;

    public override ESliderType Type { get => ESliderType.Stamina; protected set { } }

    public bool SubtractStamina(int value)
    {
        if (currentValue - value < 0) return false;
        if (_staminaCoroutine != null)
        {
            StopCoroutine(_staminaCoroutine);
            _staminaCoroutine = null;
        }
        _lastTimeShot = Time.time + _staminaDelay;
        Current -= value;
        hasShot = true;
        return true;
    }
    private IEnumerator StaminaAdd()
    {
        while (currentValue < maxValue)
        {
            Current += 1;
            yield return new WaitForSeconds(_regenSpeed);
        }
        Current = maxValue;
        _staminaCoroutine = null;
    }
    public void ForManualMode()
    {
        RegisterUpdate();
    }
    public void ForAIMode()
    {
        UnregisterUpdate();
    }
    #region Update
    public void PerformInitialUpdate()
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
    public void PerformPreUpdate()
    {
        throw new System.NotImplementedException();
    }
    public void PerformUpdate()
    {
        throw new System.NotImplementedException();
    }
    public void PerformFinalUpdate()
    {
        throw new System.NotImplementedException();
    }
    public void PerformLateUpdate()
    {
        throw new System.NotImplementedException();
    }
    private void RegisterUpdate()
    {
        Updater.Instance!.RegisterUpdate(this, Updater.UpdateType.InitialUpdate);
    }
    private void UnregisterUpdate()
    {
        Updater.Instance!.UnregisterUpdate(this, Updater.UpdateType.InitialUpdate);
    }
    #endregion
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