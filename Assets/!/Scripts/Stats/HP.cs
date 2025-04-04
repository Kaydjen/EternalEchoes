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




public class HPRecovery : MonoBehaviour, IGameplayModeSwitcher
{
    public event Action<int> OnCurrentChanged;

    [Header("One point = one player hp = enemy soul's cost")]
    [SerializeField] private List<int> _levels = new(); 
    private int _currentValue;
    private int _currentLevel;
    public void Recovery()
    {
        if(TryGetComponent(out HP hp))
        {

            hp.GetRecovery(_currentValue);
            
        }
    }
    public void Replenish(int value)
    {
        if(_currentValue + value > _levels[_currentLevel]) // hp > maxHP
        {
            int remainder = _currentValue + value - _levels[_currentLevel];
            _currentLevel++;
            _currentValue = remainder;
        }
        else if(_currentValue + value == _levels[_currentLevel]) // hp == maxHP
        {
            _currentLevel++;
            _currentValue = 0;
        }
        else // hp < maxHP
        {
            _currentValue += value;
        }
        OnCurrentChanged?.Invoke(_currentValue);
    }
    public List<int> GetLevels() => _levels;
    public void ForAIMode()
    {

    }
    public void ForManualMode()
    {

    }
}
