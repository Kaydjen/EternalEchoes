using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    [SerializeField] private MonoBehaviour _sliderValueSource;
    private ISliderValue _sliderValue;
    [SerializeField] private Slider _slider;

    private void Start()
    {
        _slider = GetComponent<Slider>();

        // Приводимо об'єкт до інтерфейсу ISliderValue
        _sliderValue = _sliderValueSource as ISliderValue;

        if (_sliderValue != null)
        {
            // Підписуємося на подію зміни значення
            _sliderValue.OnValueChanged += UpdateSlider;
            UpdateSlider(_sliderValue.Current, _sliderValue.Max);
        }
        else
        {
            Debug.LogError("Об'єкт не реалізує ISliderValue!");
        }
    }

    private void UpdateSlider(float currentValue, float maxValue)
    {
        _slider.maxValue = maxValue;
        _slider.value = currentValue;
    }
}
