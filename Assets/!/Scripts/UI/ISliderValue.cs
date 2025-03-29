using System;
using UnityEngine.Events;

public interface ISliderValue
{
    public float Current { get;  set; }
    public float Max { get; set; }

    event Action<float, float> OnValueChanged;
}
