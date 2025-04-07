using UnityEngine;

public class IciclePool : Pool<Transform>
{
    public static IciclePool Instance;

    private void Awake()
    {
        Instance = this;
    }
}
