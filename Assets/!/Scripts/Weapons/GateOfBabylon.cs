using UnityEngine;


public class GateOfBabylon : AbstractWeapon
{
    private GateOfBabylonScriptable _data;
    protected override void Start() => _data = Data as GateOfBabylonScriptable;
    public override void Attack()
    {
        Debug.Log("GateOfBabylon, swords cound = " + _data.MinSwordsCount);
    }
}
