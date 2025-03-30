using System;
using TMPro;
using UnityEngine;

[Serializable]
public class StatsSerializable 
{
    public ESliderType Type;
    public StatsComponentsList Components;
}

[Serializable]
public class StatsComponentsList
{
    public TMP_Text Max;
    public TMP_Text Current;
    public GameObject PartsObj;
}