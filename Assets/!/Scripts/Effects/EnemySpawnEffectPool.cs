using UnityEngine;

public class EnemySpawnEffectPool : ObjectPool
{
    public static EnemySpawnEffectPool Instance;
    private void Awake()
    {
        Instance = this;
    }
    public void SetEffect(Vector3 position, Vector3 scale, float duration = 1f)
    {
        GameObject effect = base.GetObject();

        effect.transform.position = position;
        effect.transform.localScale = scale;
        base.DelayedReturnObject(effect, duration);
    }
    public void SetEffect(Vector3 position, float duration = 1f)
    {
        GameObject effect = base.GetObject();

        effect.transform.position = position;
        base.DelayedReturnObject(effect, duration);
    }
}
