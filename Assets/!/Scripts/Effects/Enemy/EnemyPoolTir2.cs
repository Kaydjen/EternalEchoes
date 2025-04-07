using UnityEngine;

public class EnemyPoolTir2 : Pool<Transform>
{
    public static EnemyPoolTir2 Instance;

    private void Awake()
    {
        Instance = this;
    }
}
