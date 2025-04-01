using UnityEngine;
using UnityEngine.AI;

public class TornadoProjectile : MonoBehaviour
{
    private Rigidbody _rb;
    [SerializeField] private float _speed;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    private void OnEnable()
    {
        
        if (AimDirection.Direction == null) return;
        _rb.velocity = Vector3.zero;
        _rb.AddForce(LookOrientation.Direction.forward * _speed, ForceMode.Impulse);
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            NavMeshAgent agent = other.GetComponent<NavMeshAgent>();
            TornadoPull.Instance.Pull(agent, gameObject);
        }
    }

    public void OnParticleSystemStopped()
    {
        TornadoPool.Instance.Return(this.transform);
    }

}
