using UnityEngine;

[ComponentInfo("", "Use it to set up Enemy stats and levels")]
public class SoulsLevelsHandler : MonoBehaviour
{
    [SerializeField] private byte _currentSoulNumber;
    public SoulScriptableObject Soul1;
    public SoulScriptableObject Soul2;
    public SoulScriptableObject Soul3;
    public SoulScriptableObject Soul4;
    private SoulScriptableObject _currentSoul;
    private EnemyHP _hp;

    public byte GetCurrentSoulNumber() => _currentSoulNumber;
    public void IncreaseSoulLevel()
    {
        if (_currentSoulNumber + 1 >= 4) return;
        _currentSoulNumber++;
        SetCurrentSoul();

        _hp.SetHp(_currentSoul.HP);
    }
    public void GetParameters()
    {
        _hp = GetComponent<EnemyHP>();
    }
    public ESoulType GetCurrentSoulType() => _currentSoul.Type;
    private void SetCurrentSoul()
    {
        switch (_currentSoulNumber)
        {
            case 1: _currentSoul = Soul2; break;
            case 2: _currentSoul = Soul3; break;
            case 3: _currentSoul = Soul4; break;
        }
    }
    private void Start()
    {
        SetCurrentSoul();
    }
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
