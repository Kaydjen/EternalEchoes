using UnityEngine;

[RequireComponent(typeof(AttackHandler))]
public class ProjectileInstantiater : MonoBehaviour, IAttack
{
    [SerializeField] private EProjectile _type;
    [SerializeField] private int _manaCost;
    [SerializeField] private Vector3 _offset = new Vector3(0f, 0f, 0f);
    [SerializeField] private float _areaDist;
    private Pool<Transform> _pool;
    [SerializeField] private Stamina _stamina;
    [SerializeField] private bool _areaProjectile;

    private void Start()
    {
        switch (_type)
        {
            case EProjectile.Fireball:
                _pool = FireballPool.Instance;
                _manaCost = 25;
                GetComponent<AttackHandler>().SetNewFireRate(0.5f);
                _offset = new Vector3(0f, 0f, 0f);
                _areaProjectile = false;
                break;
            case EProjectile.Icicle:
                _pool = IciclePool.Instance;
                _manaCost = 35;
                GetComponent<AttackHandler>().SetNewFireRate(0.5f);
                _offset = new Vector3(0f, 0f, 0f);
                _areaProjectile = false;
                break;
            case EProjectile.Tornado:
                _pool = TornadoPool.Instance;
                _manaCost = 100;
                GetComponent<AttackHandler>().SetNewFireRate(20f);
                _offset = new Vector3(0f,-1f,0f);
                _areaProjectile = false;
                break;
            case EProjectile.MeteorShower:
                _pool = MeteorShowerPool.Instance;
                _manaCost = 1;
                GetComponent<AttackHandler>().SetNewFireRate(0f);
                _offset = new Vector3(0f, 0f, 0f);
                _areaProjectile = true;
                break;
        }
    }
    public void Attack()
    {
        if (_areaProjectile == false)
        {
            if (!_stamina.SubtractStamina(_manaCost)) return;
            Transform bullet = _pool.Get();
            bullet.GetComponent<IGetAttacker>().SetAttacker(_stamina.gameObject);
            bullet.position = this.transform.position + _offset;
        }
        else
        {
            Debug.Log("Ray start");
            RaycastHit hit;
            if (Physics.Raycast(AimDirection.Direction.position, AimDirection.Direction.forward, out hit, _areaDist))
            {
                if (hit.collider.CompareTag("Ground"))
                {
                    if (!_stamina.SubtractStamina(_manaCost)) return;
                    Debug.Log("Ray rgthfytdgrftyghftgrfwetyujtyhtgrewtukh");
                    Transform bullet = _pool.Get();
                    bullet.GetComponent<IGetAttacker>().SetAttacker(_stamina.gameObject);
                    // Використовуємо hit.point замість hit.transform.position
                    bullet.position = hit.point + _offset; // Додайте _offset, якщо потрібно
                }
            }
        }
    }
}
