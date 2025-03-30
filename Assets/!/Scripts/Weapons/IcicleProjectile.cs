using UnityEngine;
using UnityEngine.AI;

public class IcicleProjectile : MonoBehaviour
{
    private Rigidbody _rb;
    [SerializeField] private float _speed;
    [SerializeField] private float _freezeDuration = 0.5f; // Час заморозки

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _rb.velocity = Vector3.zero;
        transform.rotation = Quaternion.LookRotation(AimDirection.Direction.forward);
        _rb.AddForce(AimDirection.Direction.forward * _speed, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            NavMeshAgent agent = other.GetComponent<NavMeshAgent>();

            FreezeCharacter.Instance.Freeze(agent, _freezeDuration);

            IciclePool.Instance.Return(this.transform);
        }
    }

}
