using UnityEngine;
using UnityEngine.AI;

public class TornadoProjectile : MonoBehaviour
{
    private Rigidbody _rb;
    [SerializeField] private float _speed;
    private ParticleSystem _ps;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _ps = GetComponentInChildren<ParticleSystem>();
    }

    private void OnEnable()
    {
        if (AimDirection.Direction == null) return;
        _rb.velocity = Vector3.zero;
        Debug.Log("Should add force");
        _rb.AddForce(LookOrientation.Direction.forward * _speed, ForceMode.Impulse);
        Debug.Log("Should add forc2e");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            NavMeshAgent agent = other.GetComponent<NavMeshAgent>();
            TornadoPull.Instance.Pull(agent);
        }
    }

    private void OnParticleSystemStopped()
    {
        TornadoPool.Instance.Return(this.transform);
    }

}
