using UnityEngine;

class SoulPoolOne : Pool<Transform>
{
    public static SoulPoolOne Instance;

    private void Awake()
    {
        Instance = this;
    }
}
