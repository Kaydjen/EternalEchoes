using UnityEngine;

[RequireComponent(typeof(AttackHandler))]
public class ProjectileInstantiater : MonoBehaviour, IAttack
{
    [SerializeField] private EProjectile _type;
    [SerializeField] private int _manaCost;
    [SerializeField] private Vector3 _offset = new Vector3(0f, 0f, 0f);
    [SerializeField] private float _areaDist;
    private Pool<Transform> _pool;
    private Stamina _stamina;
    [SerializeField] private bool _areaProjectile;

    private void Update()
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
        _stamina = this.transform.root.GetComponent<Stamina>();
    }
    public void Attack()
    {
        
        if(_areaProjectile == true)
        {
            RaycastHit hit;
            if(Physics.Raycast(AimDirection.Direction.position, AimDirection.Direction.forward, out hit, _areaDist))
            {
                Debug.Log("grthfgdrfewtyjuhytgrf");
                if (hit.collider.CompareTag("Ground"))
                {
                    if (!_stamina.SubtractStamina(_manaCost)) return;
                    Debug.DrawRay(AimDirection.Direction.position, AimDirection.Direction.forward, Color.red, _areaDist);
                    Debug.Log("Ybababa");
                    Transform bullet = _pool.Get();
                    bullet.GetComponent<IGetAttacker>().SetAttacker(this.gameObject);
                    bullet.position = hit.transform.position;
                }
            }
        }
        else
        {
            if (!_stamina.SubtractStamina(_manaCost)) return;
            Transform bullet = _pool.Get();
            bullet.GetComponent<IGetAttacker>().SetAttacker(this.gameObject);
            bullet.position = this.transform.position + _offset;
        }
    }
}
