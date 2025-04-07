using UnityEngine;

[RequireComponent(typeof(AttackHandler))]
public class ProjectileInstantiater : MonoBehaviour, IAttack
{
    [SerializeField] private EProjectile _type;
    [SerializeField] private int _manaCost;
    [SerializeField] private Vector3 _offset = new Vector3(0f,0f,0f);
    private Pool<Transform> _pool;
    private Stamina _stamina;

    private void Start()
    {
        switch (_type)
        {
            case EProjectile.Fireball:
                _pool = FireballPool.Instance;
                _manaCost = 25;
                _offset = new Vector3(0f, 0f, 0f);
                break;
            case EProjectile.Icicle:
                _pool = IciclePool.Instance;
                _manaCost = 35;
                _offset = new Vector3(0f, 0f, 0f);
                break;
            case EProjectile.Tornado:
                _pool = TornadoPool.Instance;
                _manaCost = 100;
                _offset = new Vector3(0f,-1f,0f);
                break;
            case EProjectile.Slash:
                _pool = SlashPool.Instance;
                _manaCost = 0;
                _offset = new Vector3(0f, 0, 0f);
                break;
        }
        _stamina = this.transform.root.GetComponent<Stamina>();
    }
    public void Attack()
    {
        if (!_stamina.SubtractStamina(_manaCost)) return;
        Transform bullet = _pool.Get();
        bullet.GetComponent<IGetAttacker>().SetAttacker(this.gameObject);
        bullet.position = this.transform.position + _offset;
    }
}
