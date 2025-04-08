using UnityEngine;

public class FreezeAuraPool : Pool<Transform>
{
    public static FreezeAuraPool Instance;

    private void Awake()
    {
        Instance = this;
    }
}
