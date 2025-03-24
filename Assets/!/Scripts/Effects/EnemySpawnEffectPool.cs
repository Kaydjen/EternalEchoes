using UnityEngine;

public class EnemySpawnEffectPool : ObjectPool
{
    public static EnemySpawnEffectPool Instance;
    [SerializeField] private Vector3 _defSize;
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
        effect.transform.localScale = _defSize;
        base.DelayedReturnObject(effect, duration);
    }
}
