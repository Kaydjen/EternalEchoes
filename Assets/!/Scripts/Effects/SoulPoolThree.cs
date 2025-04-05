using UnityEngine;

class SoulPoolThree : Pool<Transform>
{
    public static SoulPoolThree Instance;

    private void Awake()
    {
        Instance = this;
    }
}
