using UnityEngine;

class SoulOne : Pool<Transform>
{
    public static SoulOne Instance;

    private void Awake()
    {
        Instance = this;
    }
}
