using UnityEngine;

public class EnemySpawnEffectPool : ObjectPool
{
    public void SetEffect(Vector3 position, float duration, Vector3 scale)
    {
        GameObject effect = base.GetObject();

        effect.transform.position = position;
        effect.transform.localScale = scale;
        base.DelayedReturnObject(effect, duration);
    }
    public void SetEffect(Vector3 position, float duration)
    {
        GameObject effect = base.GetObject();

        effect.transform.position = position;
        base.DelayedReturnObject(effect, duration);
    }
}
