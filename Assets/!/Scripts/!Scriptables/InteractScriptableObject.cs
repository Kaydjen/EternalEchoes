using UnityEngine;

[CreateAssetMenu(fileName = "InteractionObjectsParameters", menuName = "ScriptableObjects/InteractionObjectsParameters", order = 1)]
public class InteractScriptableObject : ScriptableObject
{
    [Header("MAX 8")]
    public Sprite[] IconsMain;
    public Sprite[] IconsHighlighted;
    public Sprite[] IconsPressed;
}


































/*
 
    public string[] Description = 
    {
        "1.",
        "2.",
        "3.",
        "4.",
        "5.",
        "6.",
        "7.",
        "8.",
        "9.",
    };

*/
