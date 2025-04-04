using System;
using UnityEngine;

public class Stats : MonoBehaviour, IStatsValue
{
    [SerializeField] protected float maxValue;
    [SerializeField] protected float currentValue;
    public virtual ESliderType Type { get => ESliderType.Undefined; set {} }
    public virtual float Current 
    {
        get { return currentValue; }
        set
        {
            currentValue = Mathf.Clamp(value, 0f, maxValue);
        }
    }
    public virtual float Max 
    {
        get { return maxValue; }
        set
        {
            maxValue = value;
            OnValueChanged?.Invoke(currentValue, maxValue);
        }
    }

    public virtual event Action<float, float> OnValueChanged;
    public virtual void Unsubscribe() => OnValueChanged = null;
}
