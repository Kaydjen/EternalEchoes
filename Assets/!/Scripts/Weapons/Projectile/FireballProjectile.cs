using UnityEngine;

public class FireballProjectile : MonoBehaviour, IGetAttacker
{
    private Rigidbody _rb;
    [SerializeField] private float _speed;
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
        _rb.AddForce(AimDirection.Direction.forward * _speed, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            IDamageable damageable = other.GetComponent<IDamageable>();
            damageable.GetDamage(_damage, _lastAttacker);
            FireballPool.Instance.Return(this.transform);
        } 
    }

}
