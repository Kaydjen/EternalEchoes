using UnityEngine;

public class BulletPool : Pool<TrailRenderer>
{
    public static BulletPool Instance;

    private void Awake()
    {
        Instance = this;
    }
}