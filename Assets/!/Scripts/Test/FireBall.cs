using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AimHandler))]
public class FireBall : MonoBehaviour, IAim
{
    public void StartAim()
    {
        CW.I.Print("WORKS");
    }

    public void StopAim()
    {
        CW.I.Print("1WORKS");
    }
}
