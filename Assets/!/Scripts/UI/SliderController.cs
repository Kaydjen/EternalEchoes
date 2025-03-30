using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    [SerializeField] private List<SliderSerealizable> _sliders = new();
    [SerializeField] private Dictionary<ESliderType, Slider> _slidersDictionary = new();
    [SerializeField] private TMP_Text _valueText;
    private List<ISliderValue> _sliderValue = new();

    private void Start()
    {
        GameEvents.OnCharacterChange.AddListener(UpdateParameters);

        foreach (SliderSerealizable el in _sliders)
        {
            _slidersDictionary.Add(el.Type, el.Slider);
        }
    }
    private void UpdateParameters()
    {
        // clear _sliderValue mb
        _sliderValue = PlayerCore.Instance.transform.GetComponents<ISliderValue>().ToList();

        if (_sliderValue == null)
        {
            Debug.Log($"{_sliderValue.GetType().Name} is Null in {this.name}");
            return;
        }
        foreach (ISliderValue sliderValue in _sliderValue)
        {
            if (_slidersDictionary.ContainsKey(sliderValue.Type))
            {
                sliderValue.OnValueChanged += (current, max) => UpdateSlider(current, max); // maybe just += UpdateSlider(float currentValue, float maxValue)
            }
            sliderValue.OnValueChanged += UpdateSlider;
            UpdateSlider(sliderValue.Current, sliderValue.Max);
        }
    }
    private void UpdateSlider(float currentValue, float maxValue)
    {
        //_slider.maxValue = maxValue;
       // _slider.value = currentValue;
        _valueText.text = $"{currentValue}/{maxValue}";
    }
}
