using UnityEngine;

public class EnemyPoolTir1 : Pool<Transform>
{
    public static EnemyPoolTir1 Instance;

    private void Awake()
    {
        Instance = this;
    }
}
