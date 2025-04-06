using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Soul", menuName = "ScriptableObjects", order = 2)]
public class SoulScriptableObject : ScriptableObject
{
    public int HP;
    public int Damage;
    public int Speed;
    public ESoulType Type;
    public byte SoulValue;
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
