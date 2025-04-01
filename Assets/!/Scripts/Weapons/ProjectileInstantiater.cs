using UnityEngine;

[RequireComponent(typeof(AttackHandler))]
public class ProjectileInstantiater : MonoBehaviour, IAttack
{
    [SerializeField] private EProjectile _type;
    [SerializeField] private float _manaCost;
    [Tooltip("-")][SerializeField] private Vector3 _offset = new Vector3(0f,0f,0f);
    private Pool<Transform> _pool;
    private Stamina _stamina;

    private void Start()
    {
        switch (_type)
        {
            case EProjectile.Fireball:
                _pool = FireballPool.Instance;
                break;
            case EProjectile.Icicle:
                _pool = IciclePool.Instance;
                break;
            case EProjectile.Tornado:
                _pool = TornadoPool.Instance;
                break;
        }
        _stamina = this.transform.root.GetComponent<Stamina>();
    }
    public void Attack()
    {
        if (!_stamina.SubtractStamina(_manaCost)) return;
        Transform bullet = _pool.Get();
        bullet.position = this.transform.position - _offset;
    }
}
