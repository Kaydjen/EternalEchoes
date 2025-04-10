using UnityEngine;

[CreateAssetMenu(fileName = "Soul", menuName = "ScriptableObjects/Soul", order = 2)]
public class SoulScriptableObject : ScriptableObject
{
    [Header("Stats")]
    public int HP = 100;
    public int Damage = 10;
    public int Speed = 5;

    [Header("Type")]
    public ESoulType Type = ESoulType.Newborn;

    [Header("Value")]
    [Range(1, 255)] // Ограничение для Inspector
    public byte SoulValue = 1;

    private void OnValidate()
    {
        HP = Mathf.Max(1, HP);
        Damage = Mathf.Max(0, Damage);
        Speed = Mathf.Max(0, Speed);
    }
}
