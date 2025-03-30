using System.Collections;
using System.Collections.Generic;
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
            Rigidbody enemyRb = other.GetComponent<Rigidbody>();

            if (agent != null) agent.enabled = false;
            if (enemyRb != null) enemyRb.isKinematic = true;

            StartCoroutine(UnfreezeEnemy(agent, enemyRb));

            IciclePool.Instance.Return(this.transform);
        }
    }

    private IEnumerator UnfreezeEnemy(NavMeshAgent agent, Rigidbody enemyRb)
    {
        Debug.Log("Caratina da");
        yield return new WaitForSeconds(_freezeDuration);

        if (agent != null) agent.enabled = true;
        if (enemyRb != null) enemyRb.isKinematic = false;
        Debug.Log("Caratina ne");
    }
}
