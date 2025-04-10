using UnityEngine;

public class MeteorShowerPool : Pool<Transform>
{
    public static MeteorShowerPool Instance;

    private void Awake()
    {
        Instance = this;
    }
}
