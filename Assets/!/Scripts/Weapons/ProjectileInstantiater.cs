using UnityEngine;

[RequireComponent(typeof(AttackHandler))]
public class ProjectileInstantiater : MonoBehaviour, IAttack
{
    [SerializeField] private EProjectile _type;
    private Pool<Transform> _pool;
    [SerializeField] private float _manaCost;
    private Stamina _stamina;

    private void Start()
    {
        switch (_type)
        {
            case EProjectile.Fireball :
                _pool = FireballPool.Instance;
                break;
            case EProjectile.Icicle:
                _pool = IciclePool.Instance;
                break;
        }
        _stamina = this.transform.root.GetComponent<Stamina>();
    }
    public void Attack()
    {
        if (!_stamina.SubtractStamina(_manaCost)) return;
        Transform bullet = _pool.Get();
        bullet.position = this.transform.position;
    }
}
