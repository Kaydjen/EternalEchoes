using UnityEngine;

public class SlashPool : Pool<Transform>
{
    public static SlashPool Instance;

    private void Awake()
    {
        Instance = this;
    }
}

