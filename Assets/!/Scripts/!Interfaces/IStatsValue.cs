using System;
public interface IStatsValue
{
    public ESliderType Type { get; set; }
    public float Current { get;  set; }
    public float Max { get; set; }

    event Action<float, float> OnValueChanged;
    public void Unsubscribe();
}
