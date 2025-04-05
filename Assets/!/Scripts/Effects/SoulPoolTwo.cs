using UnityEngine;

class SoulPoolTwo : Pool<Transform>
{
    public static SoulPoolTwo Instance;

    private void Awake()
    {
        Instance = this;
    }
}
