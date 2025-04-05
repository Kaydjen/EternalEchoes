using UnityEngine;

class SoulThree : Pool<Transform>
{
    public static SoulThree Instance;

    private void Awake()
    {
        Instance = this;
    }
}
