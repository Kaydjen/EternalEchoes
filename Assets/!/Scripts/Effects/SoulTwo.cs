using UnityEngine;

class SoulTwo : Pool<Transform>
{
    public static SoulTwo Instance;

    private void Awake()
    {
        Instance = this;
    }
}
