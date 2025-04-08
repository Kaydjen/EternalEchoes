using UnityEngine;

public class EnemyPoolTir4 : Pool<Transform>
{
    public static EnemyPoolTir4 Instance;

    private void Awake()
    {
        Instance = this;
        InitEvents.OnEnemiesPoolsReady?.Invoke();
    }
}