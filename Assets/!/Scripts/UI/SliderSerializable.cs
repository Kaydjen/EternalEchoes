using System;
using TMPro;

[Serializable]
public class SliderSerializable 
{
    public ESliderType Type;
    public MaxCurrentTextSerializable TextValue;
}

[Serializable]
public class MaxCurrentTextSerializable
{
    public TMP_Text Max;
    public TMP_Text Current;
}