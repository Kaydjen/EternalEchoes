using UnityEngine;

[RequireComponent(typeof(AttackHandler))]
public class ProjectileInstantiater : MonoBehaviour, IAttack
{
    private Pool<Transform> _pool;

    private void Start()
    {
        _pool = FireballPool.Instance;
    }
    public void Attack()
    {
        Transform bullet = _pool.Get();
        bullet.position = this.transform.position;
    }
}
