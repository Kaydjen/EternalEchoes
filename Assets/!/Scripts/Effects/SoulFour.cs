using UnityEngine;

class SoulFour : Pool<Transform>
{
    public static SoulFour Instance;

    private void Awake()
    {
        Instance = this;
    }
}
