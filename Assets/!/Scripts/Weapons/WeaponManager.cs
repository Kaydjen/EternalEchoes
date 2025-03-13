using UnityEngine;

[CreateAssetMenu(fileName = "WeaponManager", menuName = "ScriptableObjects/WeaponScriptable", order = 2)]
public class WeaponManager : MonoBehaviour
{
    public WeaponScriptableAbstract Data;
    public void WeaponChange(WeaponScriptableAbstract data)
    {
        Data = data;
    }
}

