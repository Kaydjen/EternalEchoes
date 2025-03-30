using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SliderController : MonoBehaviour
{
    [SerializeField] private List<SliderSerializable> _sliders = new();
    private Dictionary<ESliderType, MaxCurrentTextSerializable> _slidersDictionary = new();
    private List<ISliderValue> _sliderValue = new();

    private void Start()
    {
        GameEvents.OnCharacterChange.AddListener(UpdateParameters);

        foreach (SliderSerializable el in _sliders)
        {
            _slidersDictionary.Add(el.Type, el.TextValue);
        }
        UpdateParameters();
    }

    private void UpdateParameters()
    {
        foreach (var slider in _sliderValue)
        {
            if (slider != null && _slidersDictionary.ContainsKey(slider.Type))
            {
                slider.OnValueChanged -= HandleSliderValueChanged(slider);
            }
        }

        _sliderValue = PlayerCore.Instance.transform.GetComponents<ISliderValue>().ToList();

        if (_sliderValue.Count == 0)
        {
            Debug.Log($"No {nameof(ISliderValue)} components found in {this.name}");
            return;
        }

        foreach (ISliderValue sliderValue in _sliderValue)
        {
            if (_slidersDictionary.ContainsKey(sliderValue.Type))
            {
                sliderValue.OnValueChanged += HandleSliderValueChanged(sliderValue);
            }
        }

        UpdateAllSliderValues();
    }

    private Action<float, float> HandleSliderValueChanged(ISliderValue sliderValue)
    {
        return (current, max) =>
        {
            _slidersDictionary[sliderValue.Type].Max.text = max.ToString();
            _slidersDictionary[sliderValue.Type].Current.text = current.ToString();
        };
    }

    private void UpdateAllSliderValues()
    {
        foreach (ISliderValue sliderValue in _sliderValue)
        {
            if (_slidersDictionary.ContainsKey(sliderValue.Type))
            {
                _slidersDictionary[sliderValue.Type].Max.text = sliderValue.Max.ToString();
                _slidersDictionary[sliderValue.Type].Current.text = sliderValue.Current.ToString();
            }
        }
    }
}
