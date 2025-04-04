using System;
using UnityEngine;

public class Stats : MonoBehaviour 
{
    [SerializeField] protected int maxValue;
    [SerializeField] protected int currentValue;
    public virtual ESliderType Type { get => ESliderType.Undefined; protected set {} }
    public virtual int Current 
    {
        get { return currentValue; }
        protected set
        {
            currentValue = Mathf.Clamp(value, 0, maxValue);
            OnValueChanged?.Invoke(currentValue, maxValue);
        }
    }
    public virtual int Max 
    {
        get { return maxValue; }
        protected set
        {
            maxValue = value;
            OnValueChanged?.Invoke(currentValue, maxValue);
        }
    }

    public virtual event Action<int, int> OnValueChanged;
    public virtual void Unsubscribe() => OnValueChanged = null;
}
