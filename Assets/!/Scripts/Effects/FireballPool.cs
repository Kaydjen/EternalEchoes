using UnityEngine;

public class FireballPool : Pool<Transform>
{
    public static FireballPool Instance;

    private void Awake()
    {
        Instance = this;
    }
}

