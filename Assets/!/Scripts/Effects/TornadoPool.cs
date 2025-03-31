using UnityEngine;

public class TornadoPool : Pool<Transform>
{
    public static TornadoPool Instance;

    private void Awake()
    {
        Instance = this;
    }
}
