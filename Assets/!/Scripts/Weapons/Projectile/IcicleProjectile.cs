using UnityEngine;
using UnityEngine.AI;

public class IcicleProjectile : MonoBehaviour, IGetAttacker
{
    private Rigidbody _rb;
    [SerializeField] private float _speed;
    [SerializeField] private float _freezeDuration = 0.5f;
    [SerializeField] private int _damage;
    private GameObject _lastAttacker;

    public void SetAttacker(GameObject attacker) => _lastAttacker = attacker;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        if (AimDirection.Direction == null) return;
        _rb.velocity = Vector3.zero;
        transform.rotation = Quaternion.LookRotation(AimDirection.Direction.forward);
        _rb.AddForce(AimDirection.Direction.forward * _speed, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            IDamageable damageable = other.GetComponent<IDamageable>();
            damageable.GetDamage(_damage, _lastAttacker);
            NavMeshAgent agent = other.GetComponent<NavMeshAgent>();

            FreezeCharacter.Instance.Freeze(agent, _freezeDuration);

            IciclePool.Instance.Return(this.transform);
        }
    }

}
