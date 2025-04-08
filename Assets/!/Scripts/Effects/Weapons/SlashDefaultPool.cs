using UnityEngine;

public class SlashDefaultPool : Pool<Transform>
{
    public static SlashDefaultPool Instance;

    private void Awake()
    {
        Instance = this;
    }
}
