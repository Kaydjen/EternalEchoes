using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TornadoProjectile : MonoBehaviour
{
    private Rigidbody _rb;
    [SerializeField] private float _speed;
    [SerializeField] private int _damage;
    [SerializeField] private float _damageInterval;

    private List<IDamageable> _enemies = new List<IDamageable>();
    private Coroutine _korutinaYakaNanositDamagProtivnikam;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    private void OnEnable()
    {
        _korutinaYakaNanositDamagProtivnikam = StartCoroutine(DamageDealer());
        if (AimDirection.Direction == null) return;
        _rb.velocity = Vector3.zero;
        _rb.AddForce(LookOrientation.Direction.forward * _speed, ForceMode.Impulse);
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            _enemies.Add(other.gameObject.GetComponent<IDamageable>());
            NavMeshAgent agent = other.GetComponent<NavMeshAgent>();
            TornadoPull.Instance.Pull(agent, gameObject);
        }
        
    }

    private IEnumerator DamageDealer()
    {
        yield return new WaitForSeconds(_damageInterval);
        foreach (var enemy in _enemies)
        {
            enemy.GetDamage(_damage);
        }
        _korutinaYakaNanositDamagProtivnikam = StartCoroutine(DamageDealer());
    }

    public void OnParticleSystemStopped()
    {
        StopCoroutine(_korutinaYakaNanositDamagProtivnikam);
        TornadoPool.Instance.Return(this.transform);
    }

}
