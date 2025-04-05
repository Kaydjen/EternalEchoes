using UnityEngine;

public class FireballProjectile : MonoBehaviour
{
    private Rigidbody _rb;
    [SerializeField] private float _speed;
    [SerializeField] private int _damage;

    // Start is called before the first frame update
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
            damageable.GetDamage(_damage);
            FireballPool.Instance.Return(this.transform);
        } 
    }

}
