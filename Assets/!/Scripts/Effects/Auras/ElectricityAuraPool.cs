using UnityEngine;

public class ElectricityAuraPool : Pool<Transform>
{
    public static ElectricityAuraPool Instance;

    private void Awake()
    {
        Instance = this;
    }
}
