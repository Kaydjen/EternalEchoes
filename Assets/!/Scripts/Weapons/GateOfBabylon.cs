using UnityEngine;

public class GateOfBabylon : Magic
{
    private GateOfBabylonScriptable _data;
    protected override void Start() => _data = Data as GateOfBabylonScriptable;
    public override void Attack()
    {

    }
    public override void EnterAimingMode()
    {

    }
    public override void ExitAimingMode()
    {

    }
}
