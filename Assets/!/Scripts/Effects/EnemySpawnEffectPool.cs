using UnityEngine;

public class EnemySpawnEffectPool : Pool<Transform>
{
    public static EnemySpawnEffectPool Instance;
    [SerializeField] private Vector3 _defSize;
    private const float DURATION = 10f;

    private void Awake()
    {
        Instance = this;
    }
    public void SetEffect(Vector3 position, Vector3 scale, float duration = DURATION)
    {
        Transform effect = base.Get();

        effect.transform.position = position;
        effect.transform.localScale = scale;
        base.StartCoroutine(DelayedReturn(effect, duration));
    }
    public void SetEffect(Vector3 position, float duration = DURATION)
    {
        Transform effect = base.Get();

        effect.transform.position = position;
        effect.transform.localScale = _defSize;
        base.StartCoroutine(DelayedReturn(effect, duration));
    }
}

