using UnityEngine;

[CreateAssetMenu(fileName = "InteractionObjectsParameters", menuName = "ScriptableObjects/InteractionObjectsParameters", order = 1)]
public class InteractScriptableObject : ScriptableObject
{
    //public int OptionsCount = 0;
    [Header("Don't forget to make the Description and Icon counts the same")]
    public string[] Description =
    {
        ">",
        ">",
        ">",
        ">",
        ">",
    };
    public Sprite[] Icons;
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
