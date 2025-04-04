using System;
using UnityEngine;

public class Stats : MonoBehaviour 
{
    [SerializeField] protected float maxValue;
    [SerializeField] protected float currentValue;
    public virtual ESliderType Type { get => ESliderType.Undefined; protected set {} }
    public virtual float Current 
    {
        get { return currentValue; }
        protected set
        {
            currentValue = Mathf.Clamp(value, 0f, maxValue);
            OnValueChanged?.Invoke(currentValue, maxValue);
        }
    }
    public virtual float Max 
    {
        get { return maxValue; }
        protected set
        {
            maxValue = value;
            OnValueChanged?.Invoke(currentValue, maxValue);
        }
    }

    public virtual event Action<float, float> OnValueChanged;
    public virtual void Unsubscribe() => OnValueChanged = null;
}
