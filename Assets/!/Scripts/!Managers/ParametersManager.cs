using UnityEngine;

[ComponentInfo("", "Can be used to set up Character parameters and get its components")]
public class ParametersManager : MonoBehaviour // TOKNOW: Perhabs, should be better to save taken walues to variables, but for now let it be so
{
    #region GetComponent
    public Transform GetBoth() => this.transform.GetChild(0).transform;
    public Transform GetPlayer() => this.transform.GetChild(1).transform;
    public Transform GetAI() => this.transform.GetChild(2).transform;
    public Transform GetControls() => GetPlayer().GetChild(0).transform;
    public Transform GetPlayerAnimations() => GetPlayer().GetChild(1).transform;
    #endregion
    #region SetParameter
    public void SetManualWalkSpeed(float value) => GetControls().GetComponent<DirectControlMovement>().Speed = value;
    //public void SetWeapon(AbstractWeapon<ScriptableObject> weapon) => GetBoth().GetComponent<WeaponManager>().SetUp(weapon);
    #endregion
}
