using UnityEngine;

class SoulPoolFour : Pool<Transform>
{
    public static SoulPoolFour Instance;

    private void Awake()
    {
        Instance = this;
    }
}
