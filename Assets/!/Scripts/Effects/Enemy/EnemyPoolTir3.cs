using UnityEngine;

public class EnemyPoolTir3 : Pool<Transform>
{
    public static EnemyPoolTir3 Instance;

    private void Awake()
    {
        Instance = this;
    }
}
