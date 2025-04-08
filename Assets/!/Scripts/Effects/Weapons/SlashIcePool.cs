using UnityEngine;

public class SlashIcePool : Pool<Transform>
{
    public static SlashIcePool Instance;

    private void Awake()
    {
        Instance = this;
    }
}
