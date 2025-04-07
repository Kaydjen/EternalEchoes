using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StatsController : MonoBehaviour
{
    [SerializeField] private List<StatsSerializable> _inspectorSetup = new();
    private Dictionary<ESliderType, StatsComponentsList> _inspectorSetupDictionary = new();
    private List<Stats> _statsNew = new();

    private void Start()
    {
        GameEvents.OnCharacterChange?.AddListener(UpdateParameters);

        foreach (StatsSerializable el in _inspectorSetup)
        {
            _inspectorSetupDictionary.Add(el.Type, el.Components);
        }
        UpdateParameters();
    }

    private void UpdateParameters()
    {
        if (CheckNull.Player()) return;
        foreach (var el in _statsNew) 
            el.Unsubscribe();
        _statsNew.Clear();
        foreach (var el in _inspectorSetup)
        {
            el.Components.Current.text = "";
            el.Components.Max.text = "";
            el.Components.PartsObj.SetActive(false);
        }
        _statsNew = PlayerCore.Instance.transform.GetComponents<Stats>().ToList();

        if (_statsNew.Count == 0)
        {
            Debug.Log($"No {nameof(Stats)} components found in {this.name}");
            return;
        }

        foreach (Stats sliderValue in _statsNew)
        {
            if (_inspectorSetupDictionary.ContainsKey(sliderValue.Type))
            {
                sliderValue.OnValueChanged += (current, max) => UpdateValues(sliderValue, current, max); // what need to be updated when new character 
                _inspectorSetupDictionary[sliderValue.Type].PartsObj.SetActive(true);
            }
        }

        UpdateAllSliderValues();
    }
    private void UpdateAllSliderValues()
    {
        foreach (Stats sliderValue in _statsNew)
        {
            if (_inspectorSetupDictionary.ContainsKey(sliderValue.Type)) // what need to be updated every time values is IStatsValue scripts' were changed (the same)
            {
                UpdateValues(sliderValue, sliderValue.Current, sliderValue.Max);
            }
        }
    }
    private void UpdateValues(Stats sliderValue, int current, int max)
    {
        _inspectorSetupDictionary[sliderValue.Type].Max.text = max.ToString();
        _inspectorSetupDictionary[sliderValue.Type].Current.text = current.ToString();
    }
}
